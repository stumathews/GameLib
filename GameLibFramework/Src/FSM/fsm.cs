using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibFramework.Src.FSM;
using Microsoft.Xna.Framework;

namespace GameLibFramework.Src.FSM
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
        public virtual void Enter(object owner)
        {
            Console.WriteLine($"Entering State {Name}");
        }

        public virtual void Exit(object owner)
        {
            Console.WriteLine($"Exiting State {Name}");
        }

        public virtual void Update(object owner, GameTime gameTime)
        {
            Console.WriteLine($"Entering Execute state of ' {Name}' with deltaTime of {gameTime.ElapsedGameTime}");
        }

        public string Name
        {
            get;
            set;
        }

        private List<Transition> m_Transitions = new List<Transition>();

        public List<Transition> Transitions
        {
            get { return m_Transitions; }
        }

        public void AddTransition(Transition transition)
        {
            m_Transitions.Add(transition);
        }
    }
    
    public class FSM
    {
        private object m_Owner;
        private List<State> m_States;
        private State m_CurrentState;
        
        public FSM() : this(null) { }

        public FSM(object owner)
        {
            m_Owner = owner;
            m_States = new List<State>();
            m_CurrentState = null;
        }

        public void Initialise(string stateName)
        {
            m_CurrentState = m_States.Find(state => state.Name.Equals(stateName));
            if (m_CurrentState != null)
            {
                m_CurrentState.Enter(m_Owner);
            }
        }

        public void AddState(State state)
        {
            m_States.Add(state);
        }

        public void Update(GameTime gameTime)
        {
            // Null check the current state of the FSM
            if (m_CurrentState == null) return;
            // Check the conditions for each transition of the current state
            foreach (Transition t in m_CurrentState.Transitions)
            {
                // If the condition has evaluated to true
                // then transition to the next state
                if (t.Condition())
                {
                    m_CurrentState.Exit(m_Owner);
                    m_CurrentState = t.NextState;
                    m_CurrentState.Enter(m_Owner);
                    break;
                }
            }
            // Execute the current state
            m_CurrentState.Update(m_Owner, gameTime);
        }
    
    }

    public class FleeState : State
    {
        public FleeState()
        {
            Name = "Flee";
        }

       
    }

    public class ChaseState : State
    {
        public ChaseState()
        {
            Name = "Chase";
        }

        
    }
}
