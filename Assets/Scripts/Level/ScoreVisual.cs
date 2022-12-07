using TMPro;
using UnityEngine;
namespace Level
{
    public class ScoreVisual : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void OnEnable()
        {
            //ScoreTracker.OnScoreUpdated += UpdateScoreVisual;
        }
        private void UpdateScoreVisual(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
