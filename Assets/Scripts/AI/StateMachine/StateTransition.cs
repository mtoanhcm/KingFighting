using System;

namespace KingFighting.AI
{
    public class StateTransition
    {
        public IState FromState;
        public IState ToState;
        public Func<bool> Condition;

        public StateTransition(IState from, IState to, Func<bool> condition)
        {
            FromState = from;
            ToState = to;
            Condition = condition;
        }
    }
}
