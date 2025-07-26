using KingFighting.Core;
using UnityEngine;

namespace KingFighting.AI
{
    public class StateContext
    {
        public Transform Target { get; set; }
        public SteeringBehaviour SteeringBehaviour { get; set; }
        public ICombat CombatComp { get; set; }
        public IMovement MovementComp { get; set; } 
        public IHealth HealthComp { get; set; }
    }
}
