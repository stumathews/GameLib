using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.Src.Animation
{
    public class AnimationStrip
    {
        public AnimationStrip(Texture2D texture, int frameWidth, int frameHeight, int frameCount, Color color, float scale,
            bool looping, int frameTime, int rows)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            Color = color;
            Scale = scale;
            Looping = looping;
            FrameTime = frameTime;
            Rows = rows;
        }

        public Texture2D Texture { get; }
        public int FrameWidth { get; }
        public int FrameHeight { get; }
        public int FrameCount { get; }
        public Color Color { get; }
        public float Scale { get; }
        public bool Looping { get; }
        public int FrameTime { get; }
        public int Rows { get; }
    }

    public class Animation
    {
        public enum Direction { Up = 0, Right = 1, Down = 2, Left = 3, NonDirectional = 1  }
        private bool _active;
        private Color _color;
        private int _currentFrame;
        private Rectangle _destinationRect;
        private int _elapsedTime;
        private int _frameCount;
        public int FrameHeight;
        private int _frameTime;
        public int FrameWidth;
        public bool Looping;
        public Vector2 Position;
        private float _scale;
        private Rectangle _sourceRect;
        private Texture2D _spriteStrip;
        public Direction CurrentDirection;
        public bool Idle;

        public Animation(Direction direction = Direction.NonDirectional, bool idle = true) 
        {
            Idle = idle;
            CurrentDirection = direction;
        }

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount,
            Color color, float scale, bool looping, int frameTimeMs)
        {
            _color = color;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _frameCount = frameCount;
            _scale = scale;
            Looping = looping;
            Position = position;
            _frameTime = frameTimeMs;
            _spriteStrip = texture;
            _elapsedTime = 0;
            _currentFrame = 0;
            _active = true;
        }

        /// <summary>
        ///     (uses the elapsed time to determine to switch the destination rect on the image strip)
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            if (_active == false) return;

            _elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsedTime > _frameTime)
            {
                if(!Idle)
                    _currentFrame++;
                if (_currentFrame == _frameCount)
                {
                    _currentFrame = 0;
                    if (Looping == false) _active = false;
                }

                _elapsedTime = 0;
            }

            _sourceRect = new Rectangle(_currentFrame * FrameWidth, (int) CurrentDirection * FrameHeight , FrameWidth, FrameHeight);

            _destinationRect = new Rectangle(
                (int)Position.X - (int)(FrameWidth * _scale) / 2,
                (int)Position.Y - (int)(FrameHeight * _scale) / 2,
                (int)(FrameWidth * _scale),
                (int)(FrameHeight * _scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
                spriteBatch.Draw(_spriteStrip, _destinationRect, _sourceRect, _color);
        }
    }
}

