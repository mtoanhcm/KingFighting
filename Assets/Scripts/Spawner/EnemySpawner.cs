using KingFighting.Character;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KingFighting.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig config;
        [SerializeField]
        private FighterCharacter[] fighterCharacter;

        private void Start()
        {
            SpawnCharacter();
        }

        public void SpawnCharacter()
        {
            if (fighterCharacter == null || config == null)
            {
                Debug.LogError("Cannot spawn main character because of missing config");
                return;
            }

            foreach (var character in fighterCharacter) {
                character.Spawn(new CharacterData(config));
            }
        }
    }
}
