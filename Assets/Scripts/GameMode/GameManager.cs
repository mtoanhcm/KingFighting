using KingFighting.Core;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameMode GameMode => gameMode;
        public UIGameView GamePlayView => gameplayView;

        [SerializeField]
        private GameMode gameMode;
        [SerializeField]
        private UIGameView gameplayView;

        private void Awake()
        {
            if (Instance == null) { 
                Instance = this;
            }
        }

        private void Start()
        {
            if(gameplayView.GetView<ChooseModeView>(out var chooseModeView))
            {
                chooseModeView.ChoosePlayerAmount += gameMode.InitPlayers;
            }

        }
    }
}
