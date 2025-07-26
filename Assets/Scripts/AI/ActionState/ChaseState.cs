using System;
using UnityEngine;

namespace KingFighting.AI
{
    public class ChaseState : IState
    {
        private readonly Action<Vector2> onMoveDirection;
        private readonly Transform target;
        private readonly SteeringBehaviour steeringAgent;

        public ChaseState(Transform target, SteeringBehaviour steeringAgent, Action<Vector2> OnMoveDirection)
        {
            this.target = target;
            this.steeringAgent = steeringAgent;
            onMoveDirection = OnMoveDirection;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void OnEnter()
        {
            var direction = steeringAgent.SeekToTarget(target.position);
            onMoveDirection?.Invoke(new Vector2(direction.x, direction.z));
        }

        public void Tick()
        {
            var direction = steeringAgent.SeekToTarget(target.position);
            onMoveDirection?.Invoke(new Vector2(direction.x, direction.z));
        }

        public void OnExit()
        {
            onMoveDirection?.Invoke(Vector3.zero);
        }
    }
}
