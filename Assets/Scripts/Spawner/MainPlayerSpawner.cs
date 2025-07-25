using UnityEngine;
using KingFighting.Character;

namespace KingFighting.Spawner
{
    public class MainPlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataConfig config;
        [SerializeField]
        private MainCharacter mainCharacter;

        private void Start()
        {
            SpawnMainCharacter();
        }

        public void SpawnMainCharacter()
        {
            if (mainCharacter == null || config == null)
            {
                Debug.LogError("Cannot spawn main character because of missing config");
                return;
            }

            mainCharacter.Spawn(new CharacterData(config));
        }
    }
}
