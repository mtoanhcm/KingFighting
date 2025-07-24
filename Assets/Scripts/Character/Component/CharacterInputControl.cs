using System;
using UnityEngine;
using KingFighting.Input;

namespace KingFighting.Character
{
    [DisallowMultipleComponent]
    public class CharacterInputControl : MonoBehaviour
    {
        public event Action<Vector2> OnMoveInputPerform;

        private CharacterInputMapping input;

        private void Awake()
        {
            input = new CharacterInputMapping();

            input.MoveInputPerform += OnMoveInputPerform;
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}
