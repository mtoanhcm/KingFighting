using UnityEngine;
using KingFighting.Core;

namespace KingFighting.Character
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected CharacterHealth healthComp;
        protected CharacterCombat combatComp;
        protected CharacterAnimation animationComp;
        protected CharacterMovement movementComp;
        protected CharacterSensor sensorComp;

        public virtual void Spawn(CharacterData characterData)
        {
            InitComponent(characterData);
            InitEventListener();
        }

        protected virtual void InitComponent(CharacterData data) {
            InitCombatComp(data.Damage);
            InitHealthComp(data.MaxHealth);
            InitMovementComp(data.MoveSpeed, data.CombatMoveSpeed, data.RotateSpeed);
            InitAnimationComp();
            InitCharacterSensor(data.DetectEnemyRange);
        }

        protected virtual void InitEventListener() {
            combatComp.AddListenerCombatStateChange(animationComp.UpdateCombatState);
            combatComp.AddListenerCombatStateChange(movementComp.UpdateCombatState);
            combatComp.AddListenerFocusPosInCombatChange(movementComp.UpdateLookFocusPos);

            sensorComp.AddListenerEnemyDetect(combatComp.UpdateEnemy);
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
        }

        protected virtual void InitAnimationComp()
        {
            if (animationComp == null || !TryGetComponent(out animationComp))
            {
                animationComp = gameObject.AddComponent<CharacterAnimation>();
            }
        }

        protected virtual void InitCombatComp(float damage)
        {
            if (combatComp == null || !TryGetComponent(out combatComp))
            {
                combatComp = gameObject.AddComponent<CharacterCombat>();
            }

            combatComp.Init(damage);
        }

        protected virtual void InitCharacterSensor(float detectRange)
        {
            if (sensorComp == null || !TryGetComponent(out sensorComp))
            {
                sensorComp = gameObject.AddComponent<CharacterSensor>();
            }

            var targetLayer = ObjectLayer.TargetHitLayer(LayerMask.LayerToName(gameObject.layer));
            sensorComp.Init(detectRange, targetLayer);
        }
    }
}
