using System;
using UnityEngine;
using KingFighting.Core;

namespace KingFighting.Character
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        private Action onDeath;
        private Action onTakeHit;
        public float HealthInPercent => currentHealth / maxHealth;
        public bool IsAlive => currentHealth > 0;

        private float maxHealth;
        private float currentHealth;

        public void Init(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;

            enabled = true;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= Mathf.Abs(damage);
            if (currentHealth <= 0) { 
                onDeath?.Invoke();
                return;
            }

            onTakeHit?.Invoke();
        }

        public void AddListenerTakeHit(Action action) { 
            onTakeHit -= action;
            onTakeHit += action;
        }

        public void AddListenerDeath(Action action) { 
            onDeath -= action;
            onDeath += action;
        }
    }
}
