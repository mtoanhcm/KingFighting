using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterCombat : MonoBehaviour
    {
        private Action<bool> onChangeCombatState;
        private Action<Vector3> onFocusPosChange;
        private Action onTriggerAttack;

        private float damage;
        private Transform enemy;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Update()
        {
            UpdateFocusPoint(enemy != null ? enemy.position : Vector3.zero);
        }

        public void Init(float damage)
        {
            this.damage = damage;
        }

        public void AddListenerCombatStateChange(Action<bool> action)
        {
            onChangeCombatState -= action;
            onChangeCombatState += action;
        }

        public void AddListenerFocusPosInCombatChange(Action<Vector3> action) { 
            onFocusPosChange -= action;
            onFocusPosChange += action;
        }

        public void UpdateEnemy(Transform enemy)
        {
            this.enemy = enemy;
            UpdateCombatState(enemy != null);
        }

        private void UpdateCombatState(bool isCombat)
        {
            onChangeCombatState?.Invoke(isCombat);
        }

        private void UpdateFocusPoint(Vector3 point)
        {
            onFocusPosChange?.Invoke(point);
        }
    }
}
