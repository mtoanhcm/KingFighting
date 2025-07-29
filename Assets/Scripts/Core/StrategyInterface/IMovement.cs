using UnityEngine;

namespace KingFighting.Core
{
    public interface IMovement
    {
        float MoveSpeed { get; }
        void Move(Vector2 direction);
        void StopMove(bool isStop);
    }
}
