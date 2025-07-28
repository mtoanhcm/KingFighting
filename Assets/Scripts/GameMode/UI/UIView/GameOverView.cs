using UnityEngine;
using KingFighting.Core;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using KingFighting.Character;

namespace KingFighting.GameMode
{
    public class GameOverView : UIView
    {
        [SerializeField]
        private TextMeshProUGUI resultTxt;
        [SerializeField]
        private TextMeshProUGUI levelTxt;
        [SerializeField]
        private TextMeshProUGUI buttonNameTxt;
        [SerializeField]
        private Button submitBtn;

        private const string WIN = "WIN";
        private const string LOSE = "LOSE";
        private const string NEW_GAME = "New game";
        private const string NEXT_LEVEL = "Next level";
        private const string LEVEL = "LEVEL";

        protected override void Init()
        {
            
        }

        public void SetShowGameoverLose(UnityAction loseProgressAction) {
            resultTxt.text = LOSE;
            buttonNameTxt.text = NEW_GAME;
            levelTxt.text = $"{LEVEL} {GlobalData.CurrentGameModeLevel}";

            submitBtn.onClick.RemoveAllListeners();
            submitBtn.onClick.AddListener(loseProgressAction);
        }

        public void SetShowGameoverWin(UnityAction winProgressAction) {
            resultTxt.text = WIN;
            buttonNameTxt.text = NEXT_LEVEL;
            levelTxt.text = $"{LEVEL} {GlobalData.CurrentGameModeLevel}";

            submitBtn.onClick.RemoveAllListeners();
            submitBtn.onClick.AddListener(winProgressAction);
        }
    }
}
