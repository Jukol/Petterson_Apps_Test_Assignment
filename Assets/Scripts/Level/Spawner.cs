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
        private Camera _camera;
        private IRandomizable _randomizer;
        private (int currentLevel, int currentScore) _currentLevelAndScore;
        private float _screenHeight;
        private float _screenWidth;

#region Public Methods
        
        public void Init(BackgroundManager backgroundManager)
        {
            _screenHeight = _camera.orthographicSize * 2;
            _screenWidth = _screenHeight / Screen.height * Screen.width;
            scoreTracker.OnLevelMaxScoreReached += LevelChange;
            
            InitializeRandomizer();
            
            _backgroundManager = backgroundManager;
            _backgroundManager.MakeActiveBackground(0);
            _currentLevelAndScore = ProgressTracker.GetCurrentLevelAndScore();
        }

        private void ReInit()
        {
            InitializeRandomizer();
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
        
        private void Awake()
        {
            _camera = Camera.main;
        }
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
            
            ProgressTracker.SaveCurrentLevel(_currentLevelAndScore.currentLevel + 1, scoreTracker.Score);
            
            _backgroundManager.MakeActiveBackground(_currentLevelAndScore.currentLevel + 1);
            
            ReInit();
            
            InterLevelStart();
            
            OnLevelChanged?.Invoke(levelsData.levels[_currentLevelAndScore.currentLevel].name);
        }
        
        private void InitializeRandomizer()
        {
            var currentLevelAndScore = ProgressTracker.GetCurrentLevelAndScore();
            
            RandomizeParameters parameters = new RandomizeParameters
            {
                MinInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMinimum,
                MaxInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMaximum,
                MinSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMinimum,
                MaxSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMaximum
            };

            _randomizer = new Randomizer.Randomizer(_screenHeight, _screenWidth, parameters);
        }
#endregion
        
    }
}
