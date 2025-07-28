using UnityEngine;

namespace KingFighting.Spawner
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpawnerArea : MonoBehaviour
    {
        private BoxCollider areaCollider;

        private void Awake()
        {
            areaCollider = GetComponent<BoxCollider>();
        }

        public Vector3 GetRandomPositionInArea(float radiusCheck, LayerMask avoidLayer, int maxAttempts = 30)
        {
            Vector3 center = areaCollider.center + transform.position;
            Vector3 size = Vector3.Scale(areaCollider.size, areaCollider.transform.lossyScale);

            for (int i = 0; i < maxAttempts; i++)
            {
                float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
                float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
                float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

                Vector3 randomPos = new Vector3(x, y, z);
                // Check for nearby objects in avoidLayer
                if (Physics.OverlapSphere(randomPos, radiusCheck, avoidLayer).Length == 0)
                {
                    return randomPos;
                }
            }

            return center;
        }
    }
}
