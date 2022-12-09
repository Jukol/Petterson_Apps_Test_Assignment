using Infrastructure;
using TMPro;
using UnityEngine;

namespace Level.Visual
{
    public class ScoreVisual : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private ScoreTracker scoreTracker;

        private void OnEnable()
        {
            scoreText.text = ProgressTracker.GetCurrentLevelAndScore().currentScore.ToString();
            scoreTracker.OnScoreUpdated += UpdateScoreText;
        }

        private void UpdateScoreText(int score) => 
            scoreText.text = score.ToString();
    }
}
