using UnityEngine;
using KingFighting.Core;

namespace KingFighting.AI
{
    public class CombatState : IState
    {
        public StateContext Conext => context;

        private StateContext context;

        public CombatState(StateContext context)
        {
            this.context = context;
        }

        public void OnEnter()
        {
            context.MovementComp.StopMove(true);
        }

        public void OnExit()
        {
            context.MovementComp.StopMove(false);
        }

        public void Tick()
        {
            
        }
    }
}
