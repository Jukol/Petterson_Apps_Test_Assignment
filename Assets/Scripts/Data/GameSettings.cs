using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Levels", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public SpritePoolSettings spritePoolSettings;
        public LevelData[] levels;
    }

    [Serializable]
    public class SpritePoolSettings
    {
        public int eachSpriteAmount;
        public int[] spriteSizes;
        public int checkerSize;
    }
    
    [Serializable]
    public class LevelData
    {
        public int id;
        public string name;
        public float intervalMinimum;
        public float intervalMaximum;
        public float minimumSizeFactor;
        public float maximumSizeFactor;
        public float speedFactor;
        public int maxScore;
    }
}
