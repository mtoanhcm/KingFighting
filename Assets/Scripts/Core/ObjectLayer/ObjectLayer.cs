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
    }
}
