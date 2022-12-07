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
        public string name;
        public float circleSpeed;
        public int maxScore;
    }
}
