using System;
using UnityEngine;

namespace KingFighting.AI
{
    public class ChaseState : IState
    {
        public StateContext Conext => context;

        private readonly StateContext context;

        public ChaseState(StateContext context)
        {
            this.context = context;
        }

        public void OnEnter()
        {

        }

        public void Tick()
        {
            var direction = context.SteeringBehaviour.SeekToTarget(context.Target.position);
            context.MovementComp.Move(new Vector2(direction.x, direction.z));
        }

        public void OnExit()
        {
            context.MovementComp.Move(Vector3.zero);
        }
    }
}
