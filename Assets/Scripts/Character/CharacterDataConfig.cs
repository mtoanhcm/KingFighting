using UnityEngine;

namespace KingFighting.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character/CharacterDataConfig")]
    public class CharacterDataConfig : ScriptableObject
    {
        public float MaxHealth;
        public float Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float RotateSpeed;
    }
}
