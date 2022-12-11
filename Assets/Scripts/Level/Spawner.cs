using System;
using System.Collections;
using Data;
using Infrastructure;
using Level.Randomizer;
using Level.Targets;
using Level.Visual;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        public event Action<Circle> OnCircleCreated;
        public event Action<string> OnLevelChanged;
        
        [SerializeField] public bool isRunning;
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScoreTracker scoreTracker;
        [SerializeField] private TimeTracker timeTracker;
        
        private BackgroundManager _backgroundManager;
        private IRandomizable _randomizer;
        private (int currentLevel, int currentScore) _currentLevelAndScore;
        private Services _services;

#region Public Methods
        
        public void Init(BackgroundManager backgroundManager, IRandomizable randomizer, Services services)
        {
            scoreTracker.OnLevelMaxScoreReached += LevelChange;
            
            _services = services;
            _currentLevelAndScore = _services.GetCurrentLevelAndScore();

            _randomizer = randomizer;
            
            _backgroundManager = backgroundManager;
            
            _backgroundManager.MakeActiveBackground(_currentLevelAndScore.currentLevel);
        }

        private void ReInit()
        {
            _currentLevelAndScore = _services.GetCurrentLevelAndScore();
            _services.InitRandomizer(gameSettings);
            _services.SpritePool.Init(gameSettings);
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
                targetInitParameters.Size = randomData.Size * _services.ScreenWidth;
                
                var maxScore = gameSettings.levels[_currentLevelAndScore.currentLevel].maxScore;
                var speedFactor = gameSettings.levels[_currentLevelAndScore.currentLevel].speedFactor;
                
                targetInitParameters.Speed = speedFactor / targetInitParameters.Size;
                targetInitParameters.Score = (int) (1 / targetInitParameters.Size * 10);
                targetInitParameters.MaxScore = maxScore;

                float maxSize = gameSettings.levels[_currentLevelAndScore.currentLevel].maximumSizeFactor * _services.ScreenWidth;
                int sizes = gameSettings.spritePoolSettings.spriteSizes.Length;
                float sizeDivision = maxSize / sizes;
                
                int randomSpriteFromList = Random.Range(0, gameSettings.spritePoolSettings.eachSpriteAmount);

                if (targetInitParameters.Size <= sizeDivision)
                    targetInitParameters.Sprite = _services.SpritePool.Sprites[0][randomSpriteFromList];
                else if (targetInitParameters.Size > sizeDivision && targetInitParameters.Size <= sizeDivision * 2)
                    targetInitParameters.Sprite = _services.SpritePool.Sprites[1][randomSpriteFromList];
                else if (targetInitParameters.Size > sizeDivision * 2 && targetInitParameters.Size <= sizeDivision * 3)
                    targetInitParameters.Sprite = _services.SpritePool.Sprites[2][randomSpriteFromList];
                else 
                    targetInitParameters.Sprite = _services.SpritePool.Sprites[3][randomSpriteFromList];

                yield return new WaitForSeconds(time);

                circlePrefab.Init(targetInitParameters);
                var circle = Instantiate(circlePrefab, transform);
                
                OnCircleCreated?.Invoke(circle);
            }
        }

        private void LevelChange()
        {
            if (_currentLevelAndScore.currentLevel == gameSettings.levels.Length - 1)
            {
                StopGame();
                return;
            }
            
            InterLevelStop();
            
            _services.SaveCurrentLevel(_currentLevelAndScore.currentLevel + 1, scoreTracker.Score);
            
            ReInit();
            
            _backgroundManager.MakeActiveBackground(_currentLevelAndScore.currentLevel);

            InterLevelStart();
            
            OnLevelChanged?.Invoke(gameSettings.levels[_currentLevelAndScore.currentLevel].name);
        }

#endregion
        
    }
}
