using UnityEngine;
namespace Infrastructure
{
    public static class ProgressTracker
    {
        public static void SaveCurrentLevel(int level, int score)
        {
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("Score", score);
        }

        public static (int currentLevel, int currentScore) GetCurrentLevelAndScore()
        {
            int level = PlayerPrefs.GetInt("Level", 0);
            int score = PlayerPrefs.GetInt("Score", 0);

            return (level, score);
        }
    }
}
