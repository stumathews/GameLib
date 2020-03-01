using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLib.EventDriven
{
    public class CommandManager
    {
        private readonly InputListener _inputListener;
        private readonly Dictionary<Keys,Action<GameTime>> _keyDownCommands = new Dictionary<Keys, Action<GameTime>>();
        private readonly Dictionary<Keys, Action<GameTime>> _keyUpCommands = new Dictionary<Keys, Action<GameTime>>();

        private void InputListenerOnOnKeyPressed(object sender, KeyboardEventArgs e)
        {
            if(_keyDownCommands.ContainsKey(e.Key))
                _keyDownCommands[e.Key](e.GameTime);
        }

        private void _inputListener_OnKeyUp(object sender, KeyboardEventArgs e)
        {
            if (_keyUpCommands.ContainsKey(e.Key))
                _keyUpCommands[e.Key](e.GameTime);
        }

        public CommandManager()
        {
            _inputListener = new InputListener();
            _inputListener.OnKeyDown += InputListenerOnOnKeyPressed;
            _inputListener.OnKeyUp += _inputListener_OnKeyUp;
        }

        public event EventHandler<KeyboardEventArgs> OnKeyUp = delegate { };

        public void Update(GameTime gameTime)
        {
            _inputListener.Update(gameTime);
        }

        public void AddKeyUpCommand(Keys key, Action<GameTime> command)
        {
            _keyUpCommands.Add(key, command);
            _inputListener.SupportKey(key);
        }
        public void AddKeyDownCommand(Keys key, Action<GameTime> command)
        {
            _keyDownCommands.Add(key, command);
            _inputListener.SupportKey(key);
        }

        public void Clear()
        {
            _keyDownCommands.Clear();
            _keyUpCommands.Clear();
        }
    }
}