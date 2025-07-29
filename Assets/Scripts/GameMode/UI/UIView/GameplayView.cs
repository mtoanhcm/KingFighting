using UnityEngine;
using KingFighting.Core;
using UnityEngine.UI;
using TMPro;

namespace KingFighting.GameMode
{
    public class GameplayView : UIView
    {
        [SerializeField]
        private Image playerHealthProgressImg;
        [SerializeField]
        private TextMeshProUGUI healthAmountBGTxt;
        [SerializeField]
        private TextMeshProUGUI healthAmountTxt;

        protected override void Init()
        {
            
        }

        public void ChangeHealthAmount(float healthPercent) {
            playerHealthProgressImg.fillAmount = healthPercent;

            var fillAmount = $"{(int)(healthPercent * 100)}%";

            healthAmountBGTxt.text = fillAmount;
            healthAmountTxt.text = fillAmount;
        }
    }
}
