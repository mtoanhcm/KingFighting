using Sirenix.OdinInspector;
using UnityEngine;

namespace KingFighting.Character
{
    public class TestLevelUp : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig config;

        [Button]
        private void TestSpawnDataWithLevel(int level)
        {
            var data = new CharacterData(config, level);

            Debug.Log($"Damage: {data.Damage}");
            Debug.Log($"Health: {data.MaxHealth}");
            Debug.Log($"CooldownAttack: {data.CooldownAttack}");
            Debug.Log($"Move combat speed: {data.CombatMoveSpeed}");
        }
    }
}
