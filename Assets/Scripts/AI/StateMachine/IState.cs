using UnityEngine;

namespace KingFighting.AI

{
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
}
