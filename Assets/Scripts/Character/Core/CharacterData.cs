using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterData
    {
        public float MaxHealth { get; private set; }
        public float Damage { get; private set; }
        public float AttackRange { get; private set; }
        public float AttackSpeed { get; private set; }
        public float CooldownAttack {  get; private set; }
        public float MoveSpeed { get; private set; }
        public float CombatMoveSpeed { get; private set; }
        public float RotateSpeed { get; private set; }
        public float DetectEnemyRange { get; private set; }


        public CharacterData(CharacterDataConfig config) { 
            MaxHealth = config.MaxHealth;
            Damage = config.Damage;
            AttackRange = config.AttackRange;
            AttackSpeed = config.AttackSpeed;
            MoveSpeed = config.MoveSpeed;
            CombatMoveSpeed = config.CombatMoveSpeed;
            RotateSpeed = config.RotateSpeed;
            DetectEnemyRange = config.DetectEnemyRange;
            CooldownAttack = config.CooldownAttack;
        }
    }
}
