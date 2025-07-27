using UnityEngine;
using KingFighting.Core;

namespace KingFighting.AI
{
    public class SteeringBehaviour
    {
        private readonly Transform selfTransform;
        public Vector3 currentVelocity;

        private readonly float moveSpeed = 5f;

        // Separation
        private readonly float separationRadius = 0.5f;
        private readonly float separationStrength = 1f;
        public Collider[] neighbors;

        // Obstacle Avoidance
        private readonly float obstacleDetectDistance = 2f;
        private readonly float obstacleAvoidStrength = 3f;

        private LayerMask obstacleLayerMask;
        private LayerMask selfLayerMask;

        public SteeringBehaviour(Transform selfTransform, float moveSpeed)
        {
            this.selfTransform = selfTransform;
            this.moveSpeed = moveSpeed;

            obstacleLayerMask = ObjectLayer.ObstacleLayer;
            selfLayerMask = 1 << selfTransform.gameObject.layer;

            neighbors = new Collider[32];
        }

        public Vector3 SeekToTarget(Vector3 targetPosition, bool useSeparation = false, bool useObstacleAvoidance = false)
        {
            Vector3 moveDir = Seek(targetPosition);

            if (useSeparation)
                moveDir += Separation();

            if (useObstacleAvoidance)
                moveDir += ObstacleAvoidance(moveDir);

            return moveDir;
        }

        public Vector3 Seek(Vector3 targetPosition)
        {
            Vector3 desired = (targetPosition - selfTransform.position).normalized * moveSpeed;
            return desired - currentVelocity;
        }

        public Vector3 Separation()
        {
            Vector3 force = Vector3.zero;
            int count = Physics.OverlapSphereNonAlloc(
                selfTransform.position,
                separationRadius,
                neighbors,
                selfLayerMask);

            int validCount = 0;

            for (int i = 0; i < count; i++)
            {
                Collider col = neighbors[i];
                if (col.transform == selfTransform) { 
                    continue; 
                }

                Vector3 toOther = selfTransform.position - col.transform.position;
                float dist = toOther.magnitude;

                if (dist > 0.01f)
                {
                    force += toOther.normalized / dist;
                    validCount++;
                }
            }

            if (validCount > 0)
            {
                force /= validCount;
                force *= separationStrength;
            }

            return force;
        }

        public Vector3 ObstacleAvoidance(Vector3 moveDir)
        {
            Ray ray = new Ray(selfTransform.position, moveDir.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, obstacleDetectDistance, obstacleLayerMask))
            {
                Vector3 avoidDir = Vector3.Reflect(moveDir.normalized, hit.normal);
                return avoidDir * obstacleAvoidStrength;
            }

            return Vector3.zero;
        }
    }
}
