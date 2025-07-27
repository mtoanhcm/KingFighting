using UnityEngine;

namespace KingFighting.Core
{
    public interface IHealth
    {
        bool IsAlive { get; }
        void TakeDamage(float damage);
    }
}
