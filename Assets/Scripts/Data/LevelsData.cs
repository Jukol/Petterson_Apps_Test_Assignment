using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Levels", order = 0)]
    public class LevelsData : ScriptableObject
    {
        public LevelData[] levels;
    }

    [Serializable]
    public class LevelData
    {
        public int id;
        public string name;
        public float intervalMinimum;
        public float intervalMaximum;
        public float sizeMinimum;
        public float sizeMaximum;
        public float speedFactor;
        public int maxScore;
    }
}
