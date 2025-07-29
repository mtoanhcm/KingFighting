using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingFighting.Input
{
    public class CharacterInputMapping
    {
        public event Action<Vector2> MoveInputPerform;

        private PlayerInput inputAction;

        public CharacterInputMapping() {
            inputAction = new PlayerInput();
        }

        public void Enable()
        {
            inputAction.Enable();

            inputAction.Player.Move.performed += MoveInputHandle;
            inputAction.Player.Move.canceled += MoveInputHandle;
        }

        public void Disable() {

            inputAction.Player.Move.performed -= MoveInputHandle;
            inputAction.Player.Move.canceled -= MoveInputHandle;

            inputAction.Disable();
        }

        private void MoveInputHandle(InputAction.CallbackContext context)
        {
            MoveInputPerform?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
