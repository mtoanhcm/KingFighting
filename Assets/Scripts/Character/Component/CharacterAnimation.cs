using UnityEngine;
using KingFighting.Animation;
using System;
using KingFighting.Core;

namespace KingFighting.Character
{
    public class CharacterAnimation : CharacterComponent
    {
        private Action onTriggerHitByAnimEvent;
        private Animator animator;
        private AttackAnimationEvent attackAnimaton;

        private int moveXFactorHash;
        private int moveYFactorHash;
        private int normalMoveFactorHash;
        private int isInCombatStateHash;
        private int attackHash;
        private int victoryHash;
        private int knockoutHash;
        private int hitHash;

        private bool isInCombat;

        private void Awake()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            animator.speed = 1.5f;

            attackAnimaton = gameObject.GetComponentInChildren<AttackAnimationEvent>();
            if(attackAnimaton != null)
            {
                attackAnimaton.OnDealDamage -= OnDealDamageByAnimEvent;
                attackAnimaton.OnDealDamage += OnDealDamageByAnimEvent;
            }


            moveXFactorHash = Animator.StringToHash("MoveX");
            moveYFactorHash = Animator.StringToHash("MoveY");
            normalMoveFactorHash = Animator.StringToHash("NormalMove");
            isInCombatStateHash = Animator.StringToHash("IsCombat");
            attackHash = Animator.StringToHash("Attack");
            victoryHash = Animator.StringToHash("Victory");
            knockoutHash = Animator.StringToHash("KnockOut");
            hitHash = Animator.StringToHash("Hit");
        }

        public void Init()
        {
            enabled = true;
        }

        public void UpdateMoveFactoByInputDirection(Vector2 moveInputDirect)
        {
            if (!isActiveComponent)
            {
                moveInputDirect = Vector2.zero;
            }

            if (isInCombat)
            {
                animator.SetFloat(moveXFactorHash, moveInputDirect.x);
                animator.SetFloat(moveYFactorHash, moveInputDirect.y);
                animator.SetFloat(normalMoveFactorHash, 0);
            } else
            {
                animator.SetFloat(normalMoveFactorHash, moveInputDirect.magnitude);
            }
        }

        public void AddListenerTriggerHitByAnimEvent(Action action)
        {
            onTriggerHitByAnimEvent -= action;
            onTriggerHitByAnimEvent += action;
        }

        public void UpdateCombatState(bool isInCombat)
        {
            this.isInCombat = isInCombat;
            animator.SetBool(isInCombatStateHash, isInCombat);
        }

        public void TriggerAttack()
        {
            animator.SetTrigger(attackHash);
        }

        public void TriggerHit()
        {
            animator.SetTrigger(hitHash);
        }

        public void TriggerKnockOut()
        {
            animator.SetTrigger(knockoutHash);
        }

        public void TriggerVictory()
        {
            animator.SetTrigger(victoryHash);
        }

        private void OnDealDamageByAnimEvent()
        {
            onTriggerHitByAnimEvent?.Invoke();
        }
    }
}
