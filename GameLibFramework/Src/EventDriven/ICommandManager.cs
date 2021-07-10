using GameLib.EventDriven;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameLibFramework.EventDriven
{
    public interface ICommandManager
    {
        event EventHandler<KeyboardEventArgs> OnKeyUp;

        void AddKeyDownCommand(Keys key, Action<GameTime> command);
        void AddKeyUpCommand(Keys key, Action<GameTime> command);
        void Clear();
        void Update(GameTime gameTime);
    }
}