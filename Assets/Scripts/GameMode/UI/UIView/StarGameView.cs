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

        protected override void Init()
        {
            
        }

        internal void UpdateCountdown(string countDown)
        {
            countdownTxt.text = countDown;
        }
    }
}
