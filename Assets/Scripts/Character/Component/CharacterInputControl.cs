using System;
using UnityEngine;
using KingFighting.Input;

namespace KingFighting.Character
{
    [DisallowMultipleComponent]
    public class CharacterInputControl : MonoBehaviour
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
