using UnityEngine;
using KingFighting.Character;

namespace KingFighting.Spawner
{
    public class MainPlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig config;
        [SerializeField]
        private MainCharacter mainCharacterPrefab;

        public MainCharacter SpawnMainCharacter()
        {
            if (mainCharacterPrefab == null || config == null)
            {
                Debug.LogError("Cannot spawn main character because of missing config");
                return null;
            }

            var character = Instantiate(mainCharacterPrefab, transform.position, Quaternion.identity);
            character.Spawn(new CharacterData(config));

            return character;
        }
    }
}
