using KingFighting.Core;
using System;
using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterCombat : CharacterComponent, ICombat
    {
        public float AttackRange => attackRange;

        private Action<bool> onChangeCombatState;
        private Action<Vector3> onFocusPosChange;
        private Action onTriggerAttack;

        private float damage;
        private float attackRange;
        private float cooldownAttack;
        private Transform enemy;
        private LayerMask targetMask;
        private Collider[] hits;
        private float tempCooldownAttack;

        private const float ANGLE_DETECT_DAMAGE = 15;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Update()
        {
            if (!isActiveComponent)
            {
                return;
            }

            UpdateFocusPoint(enemy != null ? enemy.position : Vector3.zero);
            AutoAttack();
        }

        public void Init(float damage, float attackRange, float cooldownAttack, LayerMask targetMask)
        {
            this.damage = damage;
            this.attackRange = attackRange;
            this.targetMask = targetMask;
            this.cooldownAttack = cooldownAttack;

            hits = new Collider[5];
            enabled = true;
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

        public void AddListenerTriggerAttack(Action action)
        {
            onTriggerAttack -= action;
            onTriggerAttack += action;
        }

        public void UpdateEnemy(Transform enemy)
        {
            this.enemy = enemy;
            UpdateCombatState(enemy != null);
        }

        public void ApplyDamageArea()
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, attackRange, hits, targetMask) < 0)
            {
                return;
            }

            float cosHalfAngle = Mathf.Cos(ANGLE_DETECT_DAMAGE * 0.5f * Mathf.Deg2Rad);
            foreach (var hit in hits)
            {

                if (hit == null)
                {
                    continue;
                }

                if (!hit.gameObject.TryGetComponent(out CharacterHealth health))
                {
                    continue;
                }

                var toTarget = (hit.transform.position - transform.position).normalized;
                var dot = Vector3.Dot(transform.forward, toTarget);

                if (dot >= cosHalfAngle)
                {
                    health.TakeDamage(damage);
                }
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 origin = transform.position;
            float halfAngle = ANGLE_DETECT_DAMAGE * 0.5f;

            // Draw left and right cone edges
            Vector3 leftDir = Quaternion.AngleAxis(-halfAngle, transform.up) * transform.forward;
            Vector3 rightDir = Quaternion.AngleAxis(halfAngle, transform.up) * transform.forward;

            Gizmos.DrawLine(origin, origin + leftDir * attackRange);
            Gizmos.DrawLine(origin, origin + rightDir * attackRange);

            // Draw center line
            Gizmos.DrawLine(origin, origin + transform.forward * attackRange);
        }
#endif


        private void AutoAttack()
        {
            if (enemy == null || (enemy.position - transform.position).sqrMagnitude > attackRange)
            {
                return;
            }

            if(tempCooldownAttack > Time.time)
            {
                return;
            }

            onTriggerAttack?.Invoke();

            tempCooldownAttack = Time.time + cooldownAttack;
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
