using KingFighting.Character;
using KingFighting.Core;
using System;
using TMPro;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class StarGameView : UIView
    {
        [SerializeField]
        private TextMeshProUGUI countdownTxt;
        [SerializeField]
        private TextMeshProUGUI levelTxt;

        private const string LEVEL = "LEVEL";

        protected override void Init()
        {
            
        }

        public override void Show()
        {
            base.Show();

            levelTxt.text = $"{LEVEL} {GlobalData.CurrentGameModeLevel}";
        }

        internal void UpdateCountdown(string countDown)
        {
            countdownTxt.text = countDown;
        }
    }
}
