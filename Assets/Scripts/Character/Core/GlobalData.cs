using UnityEngine;

namespace KingFighting.Character
{
    public static class GlobalData
    {
        public static bool HasNextLevel =>
            CurrentGameModeLevel > 1 &&
            TeammatePlayerAmountPick > 0 &&
            CurrentGameModeLevel > 0;

        public static int CurrentGameModeLevel { get; private set; } = 1;
        public static int TeammatePlayerAmountPick { get; private set; } = 0;
        public static int EnemyPlayerAmountPick { get; private set; } = 0;

        public static void Reset() {
            CurrentGameModeLevel = 1;
            TeammatePlayerAmountPick = 0;
            CurrentGameModeLevel = 0;
        }

        public static void UpdateGameLevel() {
            CurrentGameModeLevel++;
        }

        public static void UpdateTeammatePlayerAmount(int amount)
        {
            TeammatePlayerAmountPick = amount;
        }

        public static void UpdateEnemyPlayerAmount(int amount) {
            EnemyPlayerAmountPick = amount; 
        }
    }
}
