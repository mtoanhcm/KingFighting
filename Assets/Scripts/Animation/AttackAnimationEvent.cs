using System;
using UnityEngine;

namespace KingFighting.Animation
{
    public class AttackAnimationEvent : MonoBehaviour
    {
        public Action OnDealDamage;

        public void DealDamageEvent()
        {
            OnDealDamage?.Invoke();
        }
    }
}
