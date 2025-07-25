using UnityEngine;

namespace KingFighting.Core
{
    public static class ObjectLayer
    {
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string EnemyDeathLayerName = "EnemyDeath";
        public const string ObstacleLayerName = "Obstacle";

        public static LayerMask ObstacleLayer => LayerMask.GetMask(ObstacleLayerName);
        public static LayerMask DeathEnemyLayer => LayerMask.GetMask(EnemyDeathLayerName);

        public static LayerMask TargetHitLayer(string seftLayerName) { 
            return seftLayerName switch
            {
                PlayerLayerName => LayerMask.GetMask(EnemyLayerName, ObstacleLayerName),
                EnemyLayerName => LayerMask.GetMask(PlayerLayerName, ObstacleLayerName),
                ObstacleLayerName => LayerMask.GetMask(PlayerLayerName, EnemyLayerName),
                _ => 0,
            };
        }
    }
}
