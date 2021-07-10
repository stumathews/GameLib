using GameLibFramework.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.Animation
{
    public partial class Animation
    {
        private bool _active;
        private Color _color;
        private int _currentFrame;
        private Rectangle _destinationRect;
        private int _elapsedTime;
        private int _frameCount;
        private int _frameHeight;
        private int _frameTime;
        private int _frameWidth;
        private bool _looping;
        private Vector2 _position;
        private float _scale;
        private Rectangle _sourceRect;
        private Texture2D _spriteStrip;
        public AnimationDirection CurrentAnimationDirection;
        public bool Idle;

        public Animation(AnimationDirection animationDirection = AnimationDirection.NonDirectional, bool idle = true) 
        {
            Idle = idle;
            CurrentAnimationDirection = animationDirection;
        }

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount,
            Color color, float scale, bool looping, int frameTimeMs)
        {
            _color = color;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _frameCount = frameCount;
            _scale = scale;
            _looping = looping;
            _position = position;
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
            _position.X = x;
            _position.Y = y;
            if (_active == false) return;

            _elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsedTime > _frameTime)
            {
                if(!Idle)
                    _currentFrame++;
                if (_currentFrame == _frameCount)
                {
                    _currentFrame = 0;
                    if (_looping == false) _active = false;
                }

                _elapsedTime = 0;
            }

            _sourceRect = new Rectangle(_currentFrame * _frameWidth, (int) CurrentAnimationDirection * _frameHeight , _frameWidth, _frameHeight);

            _destinationRect = new Rectangle(
                (int)_position.X - (int)(_frameWidth * _scale) / 2,
                (int)_position.Y - (int)(_frameHeight * _scale) / 2,
                (int)(_frameWidth * _scale),
                (int)(_frameHeight * _scale));
        }

        public void Draw(ISpriteBatcher spriteBatch)
        {
            if (_active)
                spriteBatch.Draw(_spriteStrip, _destinationRect, _sourceRect, _color);
        }
    }
}

