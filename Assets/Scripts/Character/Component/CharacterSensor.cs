using KingFighting.Core;
using System;
using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterSensor : CharacterComponent
    {
        private Action<Transform> onDetectEnemy;

        private float detectEnemyRange;
        private LayerMask enemyLayerMask;
        private Collider[] enemiesAround;
        private float tempCooldownCheckAround;

        private bool isInit;
        private bool isDeath;

        private readonly float TIME_CHECK_AROUND = 1f;
        private readonly int DETECTOR_LINE_SEGMENTS = 64;

        private void Update()
        {
            if (!isInit)
            {
                return;
            }

            if (tempCooldownCheckAround > Time.time) {
                return;
            }

            GetNearestEnemy();

            tempCooldownCheckAround = Time.time + TIME_CHECK_AROUND;
        }

        public void Init(float detectRange, LayerMask targetLayer) { 

            detectEnemyRange = detectRange;
            enemyLayerMask = targetLayer;

            enemiesAround = new Collider[10];

            enabled = true;
            isInit = true;
        }

        private void GetNearestEnemy() {
            int totalEnemiesAround = Physics.OverlapSphereNonAlloc(transform.position, detectEnemyRange, enemiesAround, enemyLayerMask);
            if (totalEnemiesAround <= 0) {
                onDetectEnemy?.Invoke(null);
                return;
            }

            if(totalEnemiesAround == 1)
            {
                var enemy = enemiesAround[0].transform;
                var health = enemy.GetComponent<CharacterHealth>();

                if (!health.IsAlive)
                {
                    enemy = null;
                }

                onDetectEnemy?.Invoke(enemy);
                return;
            }

            Transform nearestEnemy = null;
            float minSqrDist = float.MaxValue;
            for (int i = 0; i < totalEnemiesAround; i++)
            {
                var enemy = enemiesAround[i].transform;
                if (enemy == null) {
                    continue;
                }

                if(!enemy.TryGetComponent<CharacterHealth>(out var health) || !health.IsAlive)
                {
                    continue;
                }

                float sqrDist = Vector3.SqrMagnitude(enemy.position - transform.position);
                if (sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    nearestEnemy = enemy;
                }
            }

            onDetectEnemy?.Invoke(nearestEnemy);
        }

        public void AddListenerEnemyDetect(Action<Transform> action) { 
            onDetectEnemy -= action;
            onDetectEnemy += action;
        }

        public void DrawDetectLine() {

            if (!TryGetComponent<LineRenderer>(out var detecterLine)) {
                detecterLine = gameObject.AddComponent<LineRenderer>();
            }

            detecterLine.loop = true;
            detecterLine.useWorldSpace = false;
            detecterLine.widthMultiplier = 0.05f;

            DrawDetectLine();

            detecterLine.positionCount = DETECTOR_LINE_SEGMENTS;
            for (int i = 0; i < DETECTOR_LINE_SEGMENTS; i++)
            {
                float angle = 2 * Mathf.PI * i / DETECTOR_LINE_SEGMENTS;
                float x = Mathf.Cos(angle) * detectEnemyRange;
                float z = Mathf.Sin(angle) * detectEnemyRange;
                detecterLine.SetPosition(i, new Vector3(x, 0.01f, z));
            }
        }
    }
}
