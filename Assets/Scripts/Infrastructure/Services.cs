using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Level;
using Level.Randomizer;
using UnityEngine;
using WWW;

namespace Infrastructure
{
    public class Services : IServices
    {
        public float ScreenWidth => _screenWidth;
        public SpritePool SpritePool => _spritePool;
        
        private readonly float _screenHeight;
        private readonly float _screenWidth;
        private readonly Spawner _spawner;
        private readonly BackgroundDownloader _backgroundDownloader;
        private readonly BackgroundContainer _backgroundContainer;
        private readonly BackgroundManager _backgroundManager;
        private readonly GameSettings _gameSettings;
        private readonly string _bundleUrl;
        
        private IRandomizable _randomizeService;
        private SpritePool _spritePool;

        public Services(InitializationParameters initParameters, SpritePool spritePool)
        {
            _screenHeight = initParameters.ScreenHeight;
            _screenWidth = initParameters.ScreenWidth;
            _spawner = initParameters.Spawner;
            _backgroundDownloader = initParameters.BackgroundDownloader;
            _backgroundContainer = initParameters.BackgroundContainer;
            _backgroundManager = initParameters.BackgroundManager;
            _gameSettings = initParameters.GameSettings;
            _bundleUrl = initParameters.BundleUrl;
            _spritePool = spritePool;

            CreateNewSpritePool();
        }

        public void CreateNewSpritePool()
        {
            _spritePool = new SpritePool(_gameSettings);
        }

        public void InitRandomizer(GameSettings gameSettings)
        {
            var currentLevelAndScore = GetCurrentLevelAndScore();
            
            var randomizeParameters = new RandomizeParameters
            {
                MinInterval = gameSettings.levels[currentLevelAndScore.currentLevel].intervalMinimum,
                MaxInterval = gameSettings.levels[currentLevelAndScore.currentLevel].intervalMaximum,
                MinSize = gameSettings.levels[currentLevelAndScore.currentLevel].minimumSizeFactor,
                MaxSizeFactor = gameSettings.levels[currentLevelAndScore.currentLevel].maximumSizeFactor
            };
            
            _randomizeService = new Randomizer(_screenHeight, _screenWidth, randomizeParameters);
        }

        public (int currentLevel, int currentScore) GetCurrentLevelAndScore()
        {
            int level = PlayerPrefs.GetInt("Level", 0);
            int score = PlayerPrefs.GetInt("Score", 0);

            return (level, score);
        }
        
        public void SaveCurrentLevel(int level, int score)
        {
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("Score", score);
        }

        private async Task<List<SpriteRenderer>> InitBackgrounds(string bundleUrl)
        {
            _backgroundDownloader.Init(_screenHeight, _screenWidth);
            
            return await _backgroundDownloader.DownloadBackgrounds(bundleUrl);
        }

        private void InitBackgroundManager(List<SpriteRenderer> backgrounds)
        {
            foreach (var background in backgrounds) 
                _backgroundContainer.AddBackground(background);
            
            _backgroundManager.Init(_backgroundContainer, this);
        }

        public async void StartGame()
        {
            InitRandomizer(_gameSettings);
            var backgrounds = await InitBackgrounds(_bundleUrl);
            InitBackgroundManager(backgrounds);
            _spawner.Init(_backgroundManager, _randomizeService, this);
        }
    }
}
