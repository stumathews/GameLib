using System;
using System.Collections.Generic;
using GameLib.EventDriven;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibFramework.EventDriven
{
    public class InputListener
    {
        private KeyboardState PrevKeyboardState { get; set; }
        private KeyboardState CurrentKeyboardState { get; set; }

        private readonly HashSet<Keys> _keyList;

        public event EventHandler<KeyboardEventArgs> OnKeyDown;
        public event EventHandler<KeyboardEventArgs> OnKeyUp;
        
        public InputListener()
        {
            CurrentKeyboardState = Keyboard.GetState();
            PrevKeyboardState = CurrentKeyboardState;
            _keyList = new HashSet<Keys>();
        }

        public void SupportKey(Keys key) => _keyList.Add(key);

        public void Update(GameTime gameTime)
        {
            PrevKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            FireKeyboardEvents(gameTime);
        }
        
        private void FireKeyboardEvents(GameTime gameTime)
        {
            foreach (var key in _keyList)
            {
                if (CurrentKeyboardState.IsKeyDown(key))
                    OnKeyDown?.Invoke(this, new KeyboardEventArgs(key, CurrentKeyboardState, PrevKeyboardState, gameTime));

                if (PrevKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key))
                    OnKeyUp?.Invoke(this, new KeyboardEventArgs(key, CurrentKeyboardState, PrevKeyboardState, gameTime));
            }
        }
    }
}
