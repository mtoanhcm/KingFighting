using KingFighting.Core;
using KingFighting.Spawner;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class GameMode : MonoBehaviour
    {
        private Action onGameOver;

        private ICharacter mainCharacter;
        private List<ICharacter> teammates;
        private List<ICharacter> enemies;

        [Header("Spawner")]
        [SerializeField]
        private MainPlayerSpawner mainPlayerSPawner;
        [SerializeField]
        private FighterSpawner teammteSpawner;
        [SerializeField]
        private FighterSpawner enemySpawner;

        [Header("Spawner area")]
        [SerializeField]
        private SpawnerArea teammateSpawnArea;
        [SerializeField]
        private SpawnerArea enemySpawnArea;

        [Header("Camera")]
        [SerializeField]
        private CinemachineCamera followCam;

        [Button]
        public void Init(int teammateCount, int enemyCount)
        {
            SpawnMainCharacter();
            SpawnTeammates(teammateCount);
            SpawnEnemies(enemyCount);

            GameStart();
        }

        public void AddListenerGameOver(Action action)
        {
            onGameOver -= action;
            onGameOver += action;
        }

        private void SpawnEnemies(int enemyCount)
        {
            enemies = new List<ICharacter>();

            if (enemyCount == 0)
            {
                return;
            }

            for (var i = 0; i < enemyCount; i++)
            {
                var character = enemySpawner.SpawnCharacter();
                if (character != null)
                {
                    character.transform.position = enemySpawnArea.GetRandomPositionInArea();
                    enemies.Add(character);
                }
            }
        }

        private void SpawnTeammates(int teammateCount)
        {
            teammates = new List<ICharacter>();

            if (teammateCount == 0)
            {
                return;
            }

            for(var i = 0; i < teammateCount; i++)
            {
                var character = teammteSpawner.SpawnCharacter();
                if (character != null) {

                    character.transform.position = teammateSpawnArea.GetRandomPositionInArea();
                    teammates.Add(character);
                }
            }
        }

        private void SpawnMainCharacter()
        {
            var character = mainPlayerSPawner.SpawnMainCharacter();
            if (character != null) {
                character.transform.position = mainPlayerSPawner.transform.position;

                character.AddListenerMainCharacterDeath(GameOver);

                mainCharacter = character;

            }
        }

        private void GameOver()
        {

        }

        private async void GameStart()
        {
            await Task.Delay(2000);

            followCam.Follow = mainCharacter.Self.transform;
        }
    }
}
