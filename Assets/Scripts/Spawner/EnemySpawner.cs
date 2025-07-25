using KingFighting.Character;
using UnityEngine;

namespace KingFighting.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig config;
        [SerializeField]
        private FighterCharacter fighterCharacter;

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

            fighterCharacter.Spawn(new CharacterData(config));
        }
    }
}
