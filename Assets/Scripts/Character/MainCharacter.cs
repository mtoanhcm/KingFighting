using System;
using UnityEngine;
using KingFighting.Core;

namespace KingFighting.Character
{
    public class MainCharacter : CharacterBase
    {
        private Action onMainCharacterDeath;
        private CharacterInputControl inputControlComp;

        public void AddListenerMainCharacterDeath(Action action)
        {
            onMainCharacterDeath -= action;
            onMainCharacterDeath += action;
        }

        protected override void InitComponent(CharacterData data)
        {
            base.InitComponent(data);

            InitInputControlComp();
        }

        protected override void InitEventListener()
        {
            base.InitEventListener();

            inputControlComp.AddMoveInputListener(movementComp.Move);
            inputControlComp.AddMoveInputListener(animationComp.UpdateMoveFactoByInputDirection);

            healthComp.AddListenerDeath(MainCharacterDeath);
        }

        private void InitInputControlComp()
        {
            if(inputControlComp == null || !TryGetComponent(out inputControlComp))
            {
                inputControlComp = gameObject.AddComponent<CharacterInputControl>();
                inputControlComp.Init();
            }
        }

        private void MainCharacterDeath()
        {
            onMainCharacterDeath?.Invoke();
        }
    }
}
