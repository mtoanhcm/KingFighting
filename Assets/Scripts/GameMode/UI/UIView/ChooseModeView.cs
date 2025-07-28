using KingFighting.Core;
using System;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class ChooseModeView : UIView
    {
        public event Action<int, int> ChoosePlayerAmount;

        [SerializeField]
        private InputPlayerAmountWidget[] pickAmountWidget;

        protected override void Init()
        {
            foreach (var widget in pickAmountWidget) {
                widget.OnSubmitPlayerAmountForGameMode -= SubmitPlayerAmount;
                widget.OnSubmitPlayerAmountForGameMode += SubmitPlayerAmount;
            }
        }

        private void SubmitPlayerAmount(int teammateAmount, int enemyAmount)
        {
            ChoosePlayerAmount?.Invoke(teammateAmount, enemyAmount);
        }
    }
}
