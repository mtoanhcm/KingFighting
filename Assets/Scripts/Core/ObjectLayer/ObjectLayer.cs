using UnityEngine;

namespace KingFighting.Core
{
    public static class ObjectLayer
    {
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string TeammateLayerName = "Teammate";
        public const string DeathLayerName = "Death";
        public const string ObstacleLayerName = "Obstacle";

        public static LayerMask ObstacleLayer => LayerMask.GetMask(ObstacleLayerName);
        public static LayerMask DeathEnemyLayer => LayerMask.GetMask(DeathLayerName);

        public static LayerMask TargetHitLayer(string seftLayerName) { 
            return seftLayerName switch
            {
                PlayerLayerName => LayerMask.GetMask(EnemyLayerName),
                EnemyLayerName => LayerMask.GetMask(PlayerLayerName, TeammateLayerName),
                TeammateLayerName => LayerMask.GetMask(EnemyLayerName),
                ObstacleLayerName => LayerMask.GetMask(PlayerLayerName, TeammateLayerName, EnemyLayerName),
                _ => 0,
            };
        }

        public static LayerMask NameToLayerMask(string layerName)
        {
            var layerIndex = LayerMask.NameToLayer(layerName);
            if (layerIndex < 0)
            {
                Debug.LogWarning($"Layer '{layerName}' does not exist.");
                return 0;
            }

            return (LayerMask)(1 << layerIndex);
        }

        public static LayerMask IndexToLayer(int layerIndex) {
            return (LayerMask)(1 << layerIndex);
        }
    }
}
