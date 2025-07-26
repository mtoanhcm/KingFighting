using UnityEngine;
using KingFighting.Core;

namespace KingFighting.AI
{
    public class CombatState : IState
    {
        private Transform target;
        private ICombat combatComp;
        private IMovement movementComp;

        public CombatState(Transform target, ICombat combatComp, IMovement movementComp)
        {
            this.target = target;
            this.combatComp = combatComp;
            this.movementComp = movementComp;
        }

        public void OnEnter()
        {
            movementComp.StopMove(true);
        }

        public void OnExit()
        {
            movementComp.StopMove(false);
        }

        public void Tick()
        {
            
        }
    }
}
