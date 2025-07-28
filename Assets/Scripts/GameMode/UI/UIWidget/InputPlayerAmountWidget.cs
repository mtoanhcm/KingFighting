using KingFighting.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KingFighting.GameMode
{
    public class InputPlayerAmountWidget : UIWidgetView
    {
        public event Action<int, int> OnSubmitPlayerAmountForGameMode;

        [SerializeField]
        private TMP_InputField teammateAmountInput;
        [SerializeField]
        private TMP_InputField enemyAmountInput;
        [SerializeField]
        private Button submitBtn;

        private int teammateAmount;
        private int enemyAmount;

        private const int MAX_PLAYER = 50;

        private void Awake()
        {
            teammateAmount = 1;
            enemyAmount = 1;

            if (teammateAmountInput != null)
            {
                teammateAmountInput.onEndEdit.AddListener(OnInputTeammateAmount);
                teammateAmount = 0;
            }

            if (enemyAmountInput != null) {
                enemyAmountInput.onEndEdit.AddListener(OnInputEnemyAmount);
                enemyAmount = 0;
            }

            submitBtn.onClick.AddListener(() =>
            {
                submitBtn.onClick.RemoveAllListeners();
                OnSubmitPlayerAmountForGameMode?.Invoke(teammateAmount, enemyAmount);
            });

            submitBtn.interactable = false;

            CheckVisibleSubmitButton();
        }

        private void OnInputEnemyAmount(string content)
        {
            if(!int.TryParse(content, out enemyAmount))
            {
                Debug.LogError("Wrong input, please input number onlye");
                return;
            }

            enemyAmount = Mathf.Clamp(enemyAmount, 0, MAX_PLAYER);
            enemyAmountInput.text = enemyAmount.ToString();

            CheckVisibleSubmitButton();
        }

        private void OnInputTeammateAmount(string content)
        {
            if (!int.TryParse(content, out teammateAmount))
            {
                Debug.LogError("Wrong input, please input number only");
                return;
            }

            teammateAmount = Mathf.Clamp(teammateAmount, 0, MAX_PLAYER);
            teammateAmountInput.text = teammateAmount.ToString();

            CheckVisibleSubmitButton();
        }

        private void CheckVisibleSubmitButton() { 
            bool isValidTeammateAmount = teammateAmountInput == null || teammateAmount > 0;
            bool isValidEnemyAmount = enemyAmountInput == null || enemyAmount > 0;

            submitBtn.interactable = isValidTeammateAmount && isValidEnemyAmount;
        }
    }
}
