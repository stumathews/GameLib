using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace GamLib.EventDriven
{
    public class InputListener
    {
        private KeyboardState PrevKeyboardState { get; set; }
        private KeyboardState CurrentKeyboardState { get; set; }
        private GamePadState PrevGamePadState { get; set; }
        private GamePadState CurrentGamePadState { get; set; }
        private MouseState PrevMouseState { get; set; }
        private MouseState CurrentMouseState { get; set; }

        public HashSet<Keys> KeyList;
        public HashSet<Buttons> ButtonsList;

        public event EventHandler<KeyboardEventArgs> OnKeyDown = delegate { };
        public event EventHandler<KeyboardEventArgs> OnKeyPressed = delegate { };
        public event EventHandler<KeyboardEventArgs> OnKeyUp = delegate { };
        public event EventHandler<GamePadEventArgs> OnGamePadPressed = delegate { };

        public InputListener()
        {
            CurrentKeyboardState = Keyboard.GetState();
            PrevKeyboardState = CurrentKeyboardState;
            KeyList = new HashSet<Keys>();
            ButtonsList = new HashSet<Buttons>();
        }

        public void SupportKey(Keys key)
        {
            KeyList.Add(key);
        }

        public void Update(GameTime gameTime)
        {
            PrevKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PrevGamePadState = CurrentGamePadState;
            CurrentGamePadState = GamePad.GetState(PlayerIndex.One);

            PrevMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            FireKeyboardEvents(gameTime);
            FireGamePadEvents(gameTime);
        }

        private void FireGamePadEvents(GameTime gameTime)
        {
            foreach (var buttons in ButtonsList)
            {
                if(CurrentGamePadState.IsButtonDown(buttons))
                    OnGamePadPressed?.Invoke(this, new GamePadEventArgs(buttons, gameTime));
            }
        }

        private void FireKeyboardEvents(GameTime gameTime)
        {
            foreach (var key in KeyList)
            {
                if (CurrentKeyboardState.IsKeyDown(key))
                {
                    OnKeyDown?.Invoke(this, new KeyboardEventArgs(key, CurrentKeyboardState, PrevKeyboardState, gameTime));
                }

                if (PrevKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key))
                {
                    OnKeyUp?.Invoke(this, new KeyboardEventArgs(key, CurrentKeyboardState, PrevKeyboardState, gameTime));
                }
            }
        }

        public void SupportButton(Buttons buttons)
        {
            ButtonsList.Add(buttons);
        }
    }

    public class GamePadEventArgs
    {
        public Buttons Buttons { get; }
        public GameTime GameTime { get; }

        public GamePadEventArgs(Buttons buttons, GameTime gameTime)
        {
            Buttons = buttons;
            GameTime = gameTime;
        }
    }
}
