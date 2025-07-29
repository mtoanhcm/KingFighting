using UnityEngine;

namespace KingFighting.AI

{
    public interface IState
    {
        StateContext Conext { get; }
        void OnEnter();
        void Tick();
        void OnExit();
    }
}
