using UnityEngine;

namespace KingFighting.Core
{
    public interface ICharacter
    {
        Transform Self { get; }
        bool IsAlive { get; }
    }
}
