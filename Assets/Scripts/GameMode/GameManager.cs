using KingFighting.Character;
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
            Application.targetFrameRate = 60;

            gameplayView.HideAllViews();

            if (CheckPlayNextLevel())
            {
                return;
            }

            PlayNewLevel();
        }

        private bool CheckPlayNextLevel() {
            if (!GlobalData.HasNextLevel)
            {
                return false;
            }

            gameMode.InitPlayers(GlobalData.TeammatePlayerAmountPick, GlobalData.EnemyPlayerAmountPick);

            return true;
        }

        private void PlayNewLevel() {
            if (gameplayView.GetView<ChooseModeView>(out var chooseModeView))
            {
                chooseModeView.ChoosePlayerAmount += UpdatePlayerPickForGlobalData;
                chooseModeView.ChoosePlayerAmount += gameMode.InitPlayers;
            }

            gameplayView.ShowView<ChooseModeView>();
        }

        private void UpdatePlayerPickForGlobalData(int teammatePick, int enemyPick) {
            GlobalData.UpdateTeammatePlayerAmount(teammatePick);
            GlobalData.UpdateEnemyPlayerAmount(enemyPick);
        }
    }
}
