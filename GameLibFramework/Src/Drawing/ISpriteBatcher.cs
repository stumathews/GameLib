using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.Drawing
{
    public interface ISpriteBatcher
    {
        void Begin();
        void End();
        void DrawString(IGameSpriteFont spriteFont, string text, Vector2 position, Color color);
        void DrawCircle(Vector2 center, float radius, int sides, Color color, float thickness);
        void DrawRectangle(Rectangle rect, Color color, float thickness);
        void DrawCircle(Vector2 center, float radius, int sides, Color color);
        void DrawLine(float x1, float y1, float x2, float y2, Color color, float thickness);
        void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color);
        void DrawRectangle(Rectangle rect, Color color);
    }
}