using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibFramework.FSM
{
    public abstract class State
    {
        protected State(string name)
        {
            Name = name;
        }
        public virtual void Enter(object owner) => OnStateChanged?.Invoke(this, StateChangeReason.Enter);

        public virtual void Exit(object owner) => OnStateChanged?.Invoke(this, StateChangeReason.Exit);

        public virtual void Update(object owner, GameTime gameTime) => OnStateChanged?.Invoke(this, StateChangeReason.Update);

        public virtual void Initialize() => OnStateChanged?.Invoke(this, StateChangeReason.Initialize);

        public event StateInformation OnStateChanged;

        public delegate void StateInformation(State state, StateChangeReason reason);

        public enum StateChangeReason { Initialize, Enter, Update, Exit };

        public string Name
        {
            get;
            set;
        }

        private readonly List<Transition> _mTransitions = new List<Transition>();

        public List<Transition> Transitions => _mTransitions;

        public void AddTransition(Transition transition) => _mTransitions.Add(transition);
    }
}