using UnityEngine;
using KingFighting.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace KingFighting.Character
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter, IGameStateAffect
    {
        public bool IsAlive => healthComp.IsAlive;

        public Transform Self => transform;

        protected CharacterHealth healthComp;
        protected CharacterCombat combatComp;
        protected CharacterAnimation animationComp;
        protected CharacterMovement movementComp;
        protected CharacterSensor sensorComp;

        protected List<CharacterComponent> components;

        public virtual void Spawn(CharacterData characterData)
        {
            components = new List<CharacterComponent>();

            InitComponent(characterData);
            InitEventListener();
        }

        protected virtual void OnDeath()
        {
            movementComp.enabled = false;
            sensorComp.enabled = false;
            combatComp.enabled = false;
            healthComp.enabled = false;

            movementComp.Disable();
        }

        protected virtual void InitComponent(CharacterData data) {
            InitCombatComp(data.Damage, data.AttackRange, data.CooldownAttack);
            InitHealthComp(data.MaxHealth);
            InitMovementComp(data.MoveSpeed, data.CombatMoveSpeed, data.RotateSpeed);
            InitAnimationComp();
            InitCharacterSensor(data.DetectEnemyRange);
        }

        protected virtual void InitEventListener() {
            combatComp.AddListenerCombatStateChange(animationComp.UpdateCombatState);
            combatComp.AddListenerCombatStateChange(movementComp.UpdateCombatState);
            combatComp.AddListenerFocusPosInCombatChange(movementComp.UpdateLookFocusPos);
            combatComp.AddListenerTriggerAttack(animationComp.TriggerAttack);

            animationComp.AddListenerTriggerHitByAnimEvent(combatComp.ApplyDamageArea);

            sensorComp.AddListenerEnemyDetect(combatComp.UpdateEnemy);

            healthComp.AddListenerTakeHit(animationComp.TriggerHit);
            healthComp.AddListenerDeath(animationComp.TriggerKnockOut);
            healthComp.AddListenerDeath(OnDeath);
        }

        protected virtual void InitHealthComp(float maxHealth)
        {
            if (healthComp == null || !TryGetComponent(out healthComp))
            {
                healthComp = gameObject.AddComponent<CharacterHealth>();
            }

            healthComp.Init(maxHealth);
        }

        protected virtual void InitMovementComp(float moveSpeed, float combatMoveSpeed, float rotateSpeed)
        {
            if (movementComp == null || !TryGetComponent(out movementComp))
            {
                movementComp = gameObject.AddComponent<CharacterMovement>();
            }

            movementComp.Init(moveSpeed, combatMoveSpeed, rotateSpeed);

            components.Add(movementComp);
        }

        protected virtual void InitAnimationComp()
        {
            if (animationComp == null || !TryGetComponent(out animationComp))
            {
                animationComp = gameObject.AddComponent<CharacterAnimation>();
            }

            components.Add(animationComp);
        }

        protected virtual void InitCombatComp(float damage, float attackRange, float cooldownAttack)
        {
            if (combatComp == null || !TryGetComponent(out combatComp))
            {
                combatComp = gameObject.AddComponent<CharacterCombat>();
            }

            var targetLayer = ObjectLayer.TargetHitLayer(LayerMask.LayerToName(gameObject.layer));
            combatComp.Init(damage, attackRange, cooldownAttack, targetLayer);

            components.Add(combatComp);
        }

        protected virtual void InitCharacterSensor(float detectRange)
        {
            if (sensorComp == null || !TryGetComponent(out sensorComp))
            {
                sensorComp = gameObject.AddComponent<CharacterSensor>();
            }

            var targetLayer = ObjectLayer.TargetHitLayer(LayerMask.LayerToName(gameObject.layer));
            sensorComp.Init(detectRange, targetLayer);

            components.Add (sensorComp);
        }

        public void OnGameStateChange(GamePlayStateType state)
        {
            bool isActiveAllComponent = state == GamePlayStateType.Start;

            foreach (var comp in components) { 
                comp.SetActive(isActiveAllComponent);
            }
        }
    }
}
