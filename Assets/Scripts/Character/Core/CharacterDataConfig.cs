using UnityEngine;

namespace KingFighting.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character/CharacterDataConfig")]
    public class CharacterDataConfig : ScriptableObject
    {
        [Header("Stats")]
        public float MaxHealth;
        public float Damage;
        public float AttackRange;
        public float CooldownAttack;
        public float MoveSpeed;
        public float CombatMoveSpeed;
        public float RotateSpeed;
        public float DetectEnemyRange;

        [Header("Level Up")]
        public float PowerIncrease;
    }
}
