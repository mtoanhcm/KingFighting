using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterData
    {
        private class StatEntry
        {
            public Func<float> Getter;
            public Action<float> Setter;
            public StatType Type;

            public StatEntry(Func<float> getter, Action<float> setter, StatType type)
            {
                Getter = getter;
                Setter = setter;
                Type = type;
            }
        }

        private enum StatType
        {
            Increase,
            Decrease
        }

        public int Level { get; private set; }
        public float MaxHealth { get; private set; }
        public float Damage { get; private set; }
        public float AttackRange { get; private set; }
        public float CooldownAttack {  get; private set; }
        public float MoveSpeed { get; private set; }
        public float CombatMoveSpeed { get; private set; }
        public float RotateSpeed { get; private set; }
        public float DetectEnemyRange { get; private set; }


        public CharacterData(CharacterDataConfig config, int level = 1) { 
            Level = level;
            MaxHealth = config.MaxHealth;
            Damage = config.Damage;
            AttackRange = config.AttackRange;
            MoveSpeed = config.MoveSpeed;
            CombatMoveSpeed = config.CombatMoveSpeed;
            RotateSpeed = config.RotateSpeed;
            DetectEnemyRange = config.DetectEnemyRange;
            CooldownAttack = config.CooldownAttack;

            if(level > 1)
            {
                LevelUp(config.PowerIncrease * (level - 1));
            }
        }

        private void LevelUp(float powerIncrease)
        {
            var stats = new List<StatEntry>
            {
                new StatEntry(() => MaxHealth, val => MaxHealth = val, StatType.Increase),
                new StatEntry(() => Damage, val => Damage = val, StatType.Increase),
                //new StatEntry(() => CooldownAttack, val => CooldownAttack = val, StatType.Decrease),
                //new StatEntry(() => CombatMoveSpeed, val => CombatMoveSpeed = val, StatType.Increase),
            };

            float[] distribution = GenerateRandomDistribution(stats.Count, powerIncrease);

            for (int i = 0; i < stats.Count; i++)
            {
                float baseValue = stats[i].Getter();
                float changeRatio = distribution[i];

                if (stats[i].Type == StatType.Increase)
                {
                    stats[i].Setter(baseValue * (1f + changeRatio));
                }
                else if (stats[i].Type == StatType.Decrease)
                {
                    stats[i].Setter(baseValue * (1f - changeRatio));
                }
            }
        }

        private float[] GenerateRandomDistribution(int count, float totalPercent)
        {
            float[] randomParts = new float[count];
            float total = 0f;

            for (int i = 0; i < count; i++)
            {
                randomParts[i] = UnityEngine.Random.Range(0.5f, 1.5f); // Random weight
                total += randomParts[i];
            }

            float[] distribution = new float[count];
            for (int i = 0; i < count; i++)
            {
                distribution[i] = (randomParts[i] / total) * totalPercent;
            }

            return distribution;
        }
    }
}
