using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameLibFramework.FSM
{
    public class Transition
    {
        public readonly State NextState;
        public readonly Func<bool> Condition;
        public Transition(State nextState, Func<bool> condition)
        {
            NextState = nextState;
            Condition = condition;
        }
    }
    
    public abstract class State
    {
        protected State(string name)
        {
            Name = name;
        }
        public virtual void Enter(object owner) => Console.WriteLine($"Entering State {Name}");

        public virtual void Exit(object owner) => Console.WriteLine($"Exiting State {Name}");

        public virtual void Update(object owner, GameTime gameTime) => Console.WriteLine($"Entering Execute state of ' {Name}' with deltaTime of {gameTime.ElapsedGameTime}");

        public virtual void Initialize() { }

        public string Name
        {
            get;
            set;
        }

        private readonly List<Transition> _mTransitions = new List<Transition>();

        public List<Transition> Transitions => _mTransitions;

        public void AddTransition(Transition transition) => _mTransitions.Add(transition);
    }
    
    public class FSM
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
