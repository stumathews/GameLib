using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GamLib.EventDriven
{
    public class CommandManager
    {
        private readonly InputListener _inputListener;

        private readonly Dictionary<Buttons, Action<GameTime>> _buttonCommands = new Dictionary<Buttons, Action<GameTime>>();
        private readonly Dictionary<Keys,Action<GameTime>> _keyCommands = new Dictionary<Keys, Action<GameTime>>();

        private void InputListenerOnOnGamePadPressed(object sender, GamePadEventArgs e) => _buttonCommands[e.Buttons](e.GameTime);
        private void InputListenerOnOnKeyPressed(object sender, KeyboardEventArgs e) => _keyCommands[e.Key](e.GameTime);
        
        public CommandManager()
        {
            _inputListener = new InputListener();
            _inputListener.OnKeyDown += InputListenerOnOnKeyPressed;
            _inputListener.OnGamePadPressed += InputListenerOnOnGamePadPressed;
        }

        

        public void Update(GameTime gameTime)
        {
            _inputListener.Update(gameTime);
        }

        public void AddCommand(Keys key, Action<GameTime> command)
        {
            _keyCommands.Add(key, command);
            _inputListener.SupportKey(key);
        }
        public void AddCommand(Buttons buttons, Action<GameTime> command)
        {
            _buttonCommands.Add(buttons, command);
            _inputListener.SupportButton(buttons);
        }
    }
}