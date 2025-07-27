using System;
using System.Collections.Generic;

namespace KingFighting.AI
{
    public class StateMachine
    {
        private IState currentState;
        private readonly List<StateTransition> transitions = new();

        public void SetInitialState(IState state)
        {
            currentState = state;
            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            transitions.Add(new StateTransition(from, to, condition));
        }

        public void Tick()
        {
            foreach (var transition in transitions)
            {
                if (transition.FromState == currentState && transition.Condition())
                {
                    ChangeState(transition.ToState);
                    break;
                }
            }

            currentState?.Tick();
        }

        public void Stop()
        {
            currentState?.OnExit();
            currentState = null;
        }

        private void ChangeState(IState newState)
        {
            if (newState == currentState) return;

            currentState?.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
    }
}
