using KingFighting.Character;
using KingFighting.Core;
using KingFighting.Spawner;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KingFighting.GameMode
{
    public class GameMode : MonoBehaviour
    {
        public GamePlayStateType CurrentGameState { 
            get { return currentGameState; }
            set {  
                currentGameState = value;
                onGameStateChanged?.Invoke(currentGameState);
            }
        } 

        private Action<GamePlayStateType> onGameStateChanged;

        private ICharacter mainCharacter;
        private List<ICharacter> teammates;
        private List<ICharacter> enemies;
        private GamePlayStateType currentGameState;

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

        private const int TIME_START_GAME = 3;

        public async void InitPlayers(int teammateCount, int enemyCount)
        {
            CurrentGameState = GamePlayStateType.Prepare;

            PrepareEnvironment(teammateCount + enemyCount);

            SpawnMainCharacter();

            teammateCount--;

            await SpawnTeammates(teammateCount);
            await SpawnEnemies(enemyCount);

            GameStart();
        }

        private void PrepareEnvironment(int totalFighters)
        {
            var scale = 1 + totalFighters / 2 * 0.1f;

            boxingRing.localScale = new Vector3(scale, 1, scale);
        }

        private async Task SpawnEnemies(int enemyCount)
        {
            enemies = new List<ICharacter>();

            if (enemyCount == 0)
            {
                return;
            }

            for (var i = 0; i < enemyCount; i++)
            {
                var spawnPos = enemySpawnArea.GetRandomPositionInArea(0.5f, ObjectLayer.NameToLayerMask("Enemy"));
                var character = enemySpawner.SpawnCharacter(spawnPos, Vector3.up * 180, GlobalData.CurrentGameModeLevel);
                if (character != null)
                {
                    onGameStateChanged += character.OnGameStateChange;
                    character.AddListenerCharacterDeath(CheckWinGame);
                    enemies.Add(character);
                }

                await Task.Delay(5);
            }
        }

        private async Task SpawnTeammates(int teammateCount)
        {
            teammates = new List<ICharacter>();

            if (teammateCount == 0)
            {
                return;
            }

            for(var i = 0; i < teammateCount; i++)
            {
                var spawnPos = teammateSpawnArea.GetRandomPositionInArea(0.5f, ObjectLayer.NameToLayerMask("Teammate"));
                var character = teammteSpawner.SpawnCharacter(spawnPos, Vector3.up, GlobalData.CurrentGameModeLevel / 2);
                if (character != null)
                {
                    onGameStateChanged += character.OnGameStateChange;
                    teammates.Add(character);
                }

                await Task.Delay(5);
            }
        }

        private void SpawnMainCharacter()
        {
            var character = mainPlayerSPawner.SpawnMainCharacter();
            if (character != null) {
                character.transform.position = mainPlayerSPawner.transform.position;

                character.AddListenerCharacterDeath(CheckLoseGame);

                onGameStateChanged += character.OnGameStateChange;

                //Register health change for UI
                if(character.TryGetComponent<CharacterHealth>(out var healthComp))
                {
                    if(GameManager.Instance.GamePlayView.GetView<GameplayView>(out var view))
                    {
                        healthComp.AddListenerTakeHit(view.ChangeHealthAmount);
                        healthComp.TakeDamage(0);
                    }
                }

                mainCharacter = character;

            }
        }

        private void CheckWinGame(CharacterBase enemyChar)
        {
            enemies.Remove(enemyChar);
            if(enemies.Count > 0)
            {
                return;
            }

            if(CurrentGameState == GamePlayStateType.End)
            {
                return;
            }

            CurrentGameState = GamePlayStateType.End;

            var view = GameManager.Instance.GamePlayView.ShowView<GameOverView>();
            view.SetShowGameoverWin(NextGameProgress);

            GlobalData.UpdateGameLevel();
        }

        private void CheckLoseGame(CharacterBase mainChar)
        {
            if (CurrentGameState == GamePlayStateType.End)
            {
                return;
            }

            CurrentGameState = GamePlayStateType.End;

            var view = GameManager.Instance.GamePlayView.ShowView<GameOverView>();
            view.SetShowGameoverLose(NextGameProgress);

            GlobalData.Reset();
        }

        private void NextGameProgress()
        {
            SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
        }

        private async void GameStart()
        {
            await Task.Delay(1000);

            followCam.Follow = mainCharacter.Self.transform;

            var startGameView = GameManager.Instance.GamePlayView.ShowView<StarGameView>();
            var countDown = TIME_START_GAME;

            while (countDown > 0) {
                startGameView.UpdateCountdown(countDown.ToString());

                await Task.Delay(1000);

                countDown--;
            }

            startGameView.UpdateCountdown("GO");

            await Task.Delay(500);

            GameManager.Instance.GamePlayView.ShowView<GameplayView>();

            CurrentGameState = GamePlayStateType.Start;
        }
    }
}
