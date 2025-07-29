using System;
using UnityEngine;
using KingFighting.Core;

namespace KingFighting.Character
{
    public class MainCharacter : CharacterBase
    {
        private CharacterInputControl inputControlComp;

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
        }

        private void InitInputControlComp()
        {
            if(inputControlComp == null || !TryGetComponent(out inputControlComp))
            {
                inputControlComp = gameObject.AddComponent<CharacterInputControl>();
                inputControlComp.Init();
            }

            components.Add(inputControlComp);
        }
    }
}
