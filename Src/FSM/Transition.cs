using System;

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
}