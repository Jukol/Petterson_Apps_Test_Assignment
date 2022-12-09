using System;
using Infrastructure;
using Level.Targets;
using UnityEngine;

namespace Level
{
    public class ScoreTracker : MonoBehaviour
    {
        public int Score => _score;
        
        public event Action<int> OnScoreUpdated;
        public event Action OnLevelMaxScoreReached;
        
        [SerializeField] private Spawner spawner;
        
        private int _score;
        private ITargetable _target;
        
        public void Reset()
        {
            if (spawner.isRunning) return;
            _score = 0;
            OnScoreUpdated?.Invoke(_score);
        }
        
        private void Awake()
        {
            _score = ProgressTracker.GetCurrentLevelAndScore().currentScore;
            spawner.OnCircleCreated += SubscribeToCircleClick;
        }

        private void SubscribeToCircleClick(ITargetable target)
        {
            _target = target;
            _target.OnTargetClicked += UpdateScore;
        }

        private void UpdateScore(int score, int levelMaxScore)
        {
            _score += score;
            OnScoreUpdated?.Invoke(_score);
            if (_score >= levelMaxScore)
                OnLevelMaxScoreReached?.Invoke();
        }
    }
}
