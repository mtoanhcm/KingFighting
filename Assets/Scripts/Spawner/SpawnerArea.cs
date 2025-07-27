using UnityEngine;

namespace KingFighting.Spawner
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpawnerArea : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider areaCollider;

        private void Awake()
        {
            areaCollider = GetComponent<BoxCollider>();
        }

        public Vector3 GetRandomPositionInArea()
        {
            Vector3 center = areaCollider.center + transform.position;
            Vector3 size = areaCollider.size;

            float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
            float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
            float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

            return new Vector3(x, y, z);
        }
    }
}
