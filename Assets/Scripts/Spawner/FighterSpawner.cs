using KingFighting.Character;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KingFighting.Spawner
{
    public class FighterSpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig[] configs;
        [SerializeField]
        private FighterCharacter fighterCharacterPrefab;

        public FighterCharacter SpawnCharacter()
        {
            if (fighterCharacterPrefab == null || configs == null || configs.Length == 0)
            {
                Debug.LogError("Cannot spawn main character because of missing config");
                return null;
            }

            var character = Instantiate(fighterCharacterPrefab, transform.position, Quaternion.identity);
            character.Spawn(new CharacterData(configs[Random.Range(0, configs.Length)]));
            return character;
        }
    }
}
