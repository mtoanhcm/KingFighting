using UnityEngine;

namespace KingFighting.Core
{
    public interface IGameStateAffect
    {
        void OnGameStateChange(GamePlayStateType state);
    }
}
