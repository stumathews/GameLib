using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.Animation
{
    /// <summary>
    /// Holds information about an amination
    /// </summary>
    public class AnimationInfo
    {
        public AnimationInfo(Texture2D texture, int frameWidth, int frameHeight, int frameCount, Color color, float scale,
            bool looping, int frameTime)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            Color = color;
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