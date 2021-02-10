using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLib.EventDriven
{
    public class KeyboardEventArgs
    {
        private readonly KeyboardState _keyboardState;
        private readonly KeyboardState _prevKeyboardState;
        public Keys Key { get; }
        public GameTime GameTime { get; }

        public KeyboardEventArgs(Keys key, KeyboardState keyboardState, KeyboardState prevKeyboardState, GameTime gameTime)
        {
            _keyboardState = keyboardState;
            _prevKeyboardState = prevKeyboardState;
            Key = key;
            GameTime = gameTime;
        }
    }
}