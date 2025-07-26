using UnityEngine;
using KingFighting.Core;


namespace KingFighting.AI
{
    public class AINoobBrain : AIBrainBase
    {
        private StateMachine stateMachine;
        private SteeringBehaviour steeringAgent;

        private IState idleState;
        private IState chaseState;
        private IState combatState;

        public override void Init(IMovement movementComp, ICombat combatComp, IHealth healthComp, System.Func<bool> onStopBrain)
        {
            base.Init(movementComp, combatComp, healthComp, onStopBrain);

            steeringAgent = new SteeringBehaviour(transform, movementComp.MoveSpeed);
            InitStateMachine();
            isInit = true;
        }
        public override void ActiveBrain()
        {
            stateMachine.Tick();
        }

        public override void StopBrain()
        {
            stateMachine.Stop();
        }

        public override void SetTarget(Transform target) {
            this.target = target;
        }

        private void InitStateMachine() {
            stateMachine = new StateMachine();
            chaseState = new ChaseState(target, steeringAgent, movementComp.Move);
            combatState = new CombatState(target, combatComp, movementComp);
            idleState = new IdleState();

            stateMachine.SetInitialState(idleState);

            stateMachine.AddTransition(chaseState, combatState, () => IsTargetInRange());
            stateMachine.AddTransition(combatState, chaseState, () => !IsTargetInRange());

            stateMachine.AddTransition(chaseState, idleState, () => target == null);
            stateMachine.AddTransition(combatState, idleState, () => target == null);

            stateMachine.AddTransition(idleState, chaseState, () => target != null);
        }

        private bool IsTargetInRange() {

            if (target == null) {
                return false;
            }

            var distanceToTarget = (target.transform.position - transform.position).sqrMagnitude;
            var attackRange = combatComp.AttackRange;

            bool isInRange = distanceToTarget < attackRange * attackRange;
            if (isInRange) { 
                
            }

            return isInRange;
        }
    }
}
