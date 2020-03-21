using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.Animation
{
    /// <summary>
    /// Holds information about an amination
    /// </summary>
    public class AnimationInfo
    {
        public const int DefaultFrameWidth = 48;
        public const int DefaultFrameHeight = 64;
        public const int DefaultFrameTime = 150;
        public const float DefaultScale = 1;
        public const int DefaultFrameCount = 3;

        public AnimationInfo(Texture2D texture, int frameWidth = DefaultFrameWidth, int frameHeight = DefaultFrameWidth, int frameCount = DefaultFrameCount, float scale = DefaultScale,
            bool looping = true, int frameTime = DefaultFrameTime)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            Color = Color.White;
            Scale = scale;
            Looping = looping;
            FrameTime = frameTime;
        }

        public Texture2D Texture { get; }
        public int FrameWidth { get; }
        public int FrameHeight { get; }
        public int FrameCount { get; }
        public Color Color { get; }
        public float Scale { get; }
        public bool Looping { get; }
        public int FrameTime { get; }
    }
}