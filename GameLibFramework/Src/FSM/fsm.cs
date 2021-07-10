using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameLibFramework.FSM
{
    public interface IFSM
    {
        void Update(GameTime gameTime);
    }

    public class FSM : IFSM
    {
        private readonly object _mOwner;
        private readonly List<State> _mStates;
        private State _mCurrentState;
        
        public FSM() : this(null) { }

        public FSM(object owner)
        {
            _mOwner = owner;
            _mStates = new List<State>();
            _mCurrentState = null;
        }

        public void Initialise(string stateName)
        {
            _mCurrentState = _mStates.Find(state => state.Name.Equals(stateName));
            _mCurrentState?.Enter(_mOwner);
        }

        public void AddState(State state)
        {
            _mStates.Add(state);
        }

        public void Update(GameTime gameTime)
        {
            // Null check the current state of the FSM
            if (_mCurrentState == null) return;

            // Check the conditions for each transition of the current state
            foreach (var transition in _mCurrentState.Transitions)
            {
                // If the condition has evaluated to true
                // then transition to the next state
                if (transition.Condition())
                {
                    _mCurrentState.Exit(_mOwner);
                    _mCurrentState = transition.NextState;
                    _mCurrentState.Enter(_mOwner);
                    break;
                }
            }
            // Execute the current state
            _mCurrentState.Update(_mOwner, gameTime);
        }    
    }        
}
