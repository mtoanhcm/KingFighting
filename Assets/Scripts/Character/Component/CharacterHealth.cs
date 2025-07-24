using System;
using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        public event Action OnDeath;
        public float HealthInPercent => currentHealth / maxHealth;

        private float maxHealth;
        private float currentHealth;

        public void Init(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= Mathf.Abs(damage);
            if (currentHealth <= 0) { 
                OnDeath?.Invoke();
            }
        }
    }
}
