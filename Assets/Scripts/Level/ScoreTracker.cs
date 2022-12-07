using TMPro;
using UnityEngine;

namespace Level
{
    public class ScoreTracker : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private TMP_Text scoreText;
        
        private int _score;
        private ITargetable _target;

        public void Reset()
        {
            if (spawner.running) return;
            
            _score = 0;
            scoreText.text = _score.ToString();
        }
        
        private void Awake()
        {
            spawner.OnCircleCreated += SubscribeToCircleClick;
        }
        
        private void SubscribeToCircleClick(ITargetable target)
        {
            _target = target;
            _target.OnTargetClicked += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _score += score;
            scoreText.text = _score.ToString();
        }
    }
}
