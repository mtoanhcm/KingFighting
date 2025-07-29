using UnityEngine;

namespace KingFighting.AI
{
    public class IdleState : IState
    {
        public StateContext Conext => null;

        public void OnEnter() { }
        public void Tick() { }
        public void OnExit() { }
    }
}
