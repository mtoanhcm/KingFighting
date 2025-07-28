using System;
using UnityEngine;
using KingFighting.Input;
using KingFighting.Core;

namespace KingFighting.Character
{
    [DisallowMultipleComponent]
    public class CharacterInputControl : CharacterComponent
    {
        private CharacterInputMapping input;

        public void Init()
        {
            input = new CharacterInputMapping();
            input.Enable();
        }

        public void AddMoveInputListener(Action<Vector2> onListenerAction)
        {
            input.MoveInputPerform -= onListenerAction;
            input.MoveInputPerform += onListenerAction;
        }

        private void OnEnable()
        {
            input?.Enable();
        }

        private void OnDisable()
        {
            input?.Disable();
        }
    }
}
