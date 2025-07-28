using KingFighting.Core;
using KingFighting.Spawner;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class GameMode : MonoBehaviour
    {
        private Action<GamePlayStateType> onGameStateChanged;

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

        [Header("Environment")]
        [SerializeField]
        private Transform boxingRing;

        public void InitPlayers(int teammateCount, int enemyCount)
        {
            onGameStateChanged?.Invoke(GamePlayStateType.Prepare);

            PrepareEnvironment(teammateCount + enemyCount);

            SpawnMainCharacter();

            teammateCount--;

            SpawnTeammates(teammateCount);
            SpawnEnemies(enemyCount);

            GameStart();
        }

        private void PrepareEnvironment(int totalFighters)
        {
            //boxingRing.localScale = new Vector3(3, 1, 3 );
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

                    onGameStateChanged += character.OnGameStateChange;

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

                    onGameStateChanged += character.OnGameStateChange;

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

                onGameStateChanged += character.OnGameStateChange;

                mainCharacter = character;

            }
        }

        private void GameOver()
        {
            onGameStateChanged?.Invoke(GamePlayStateType.End);
            GameManager.Instance.GamePlayView.ShowView<GameOverView>();
        }

        private async void GameStart()
        {
            await Task.Delay(1000);

            followCam.Follow = mainCharacter.Self.transform;

            var startGameView = GameManager.Instance.GamePlayView.ShowView<StarGameView>();
            int countDown = 120;

            while (countDown > 0) {
                startGameView.UpdateCountdown(countDown.ToString());

                await Task.Delay(1000);

                countDown--;
            }

            startGameView.UpdateCountdown("GO");

            await Task.Delay(500);

            GameManager.Instance.GamePlayView.ShowView<GameplayView>();

            onGameStateChanged?.Invoke(GamePlayStateType.Start);
        }
    }
}
