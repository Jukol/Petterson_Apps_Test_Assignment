using System;
using System.Collections;
using Data;
using Infrastructure;
using Level.Randomizer;
using Level.Targets;
using Level.Visual;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        public event Action<Circle> OnCircleCreated;
        public event Action<string> OnLevelChanged;
        
        [SerializeField] public bool isRunning;
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private LevelsData levelsData;
        [SerializeField] private ScoreTracker scoreTracker;
        [SerializeField] private TimeTracker timeTracker;
        
        private BackgroundManager _backgroundManager;
        private IRandomizable _randomizer;
        private (int currentLevel, int currentScore) _currentLevelAndScore;
        private float _screenHeight;
        private float _screenWidth;
        private Services _services;

#region Public Methods
        
        public void Init(BackgroundManager backgroundManager, IRandomizable randomizer, Services services)
        {
            scoreTracker.OnLevelMaxScoreReached += LevelChange;
            
            _currentLevelAndScore = ProgressTracker.GetCurrentLevelAndScore();

            _randomizer = randomizer;
            _services = services;
            
            _backgroundManager = backgroundManager;
            var level = _services.GetCurrentLevelAndScore();
            _backgroundManager.MakeActiveBackground(level.currentLevel);
        }

        private void ReInit()
        {
            _currentLevelAndScore = _services.GetCurrentLevelAndScore();
            _services.InitRandomizer(levelsData);
        }
        
        public void StartGame()
        {
            if (isRunning) return;
            
            timeTracker.StartTimeTracker();
            scoreTracker.Reset();
            InterLevelStart();
        }

        public void StopGame()
        {
            timeTracker.StopTimeTracker();
            InterLevelStop();
        }
        
#endregion
        
#region Unity Calls
        
        private void OnDisable()
        {
            OnCircleCreated = null;
        }
        
#endregion

#region PrivateMethods
        
        private void InterLevelStop()
        {
            isRunning = false;
            
            StopAllCoroutines();
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private void InterLevelStart()
        {
            isRunning = true;
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                var randomData = _randomizer.GetRandomData();
                var targetInitParameters = new TargetInitParameters();

                var time = randomData.Time;
                
                targetInitParameters.Place = randomData.Place;
                targetInitParameters.Color = randomData.Color;
                targetInitParameters.Size = randomData.Size;
                
                var maxScore = levelsData.levels[_currentLevelAndScore.currentLevel].maxScore;
                var speedFactor = levelsData.levels[_currentLevelAndScore.currentLevel].speedFactor;
                
                targetInitParameters.Speed = (1 * speedFactor) / targetInitParameters.Size;
                targetInitParameters.Score = (int) (1 / targetInitParameters.Size * 10);
                targetInitParameters.MaxScore = maxScore;
                
                yield return new WaitForSeconds(time);

                circlePrefab.Init(targetInitParameters);
                var circle = Instantiate(circlePrefab, transform);
                
                OnCircleCreated?.Invoke(circle);
            }
        }

        private void LevelChange()
        {
            if (_currentLevelAndScore.currentLevel == levelsData.levels.Length - 1)
            {
                StopGame();
                return;
            }
            
            InterLevelStop();
            
            _services.SaveCurrentLevel(_currentLevelAndScore.currentLevel + 1, scoreTracker.Score);
            
            ReInit();
            
            _backgroundManager.MakeActiveBackground(_currentLevelAndScore.currentLevel);

            InterLevelStart();
            
            OnLevelChanged?.Invoke(levelsData.levels[_currentLevelAndScore.currentLevel].name);
        }

#endregion
        
    }
}
