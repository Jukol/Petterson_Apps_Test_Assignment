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
        private readonly float _screenHeight;
        private readonly float _screenWidth;
        private readonly Spawner _spawner;
        private readonly BackgroundDownloader _backgroundDownloader;
        private readonly BackgroundContainer _backgroundContainer;
        private readonly BackgroundManager _backgroundManager;
        private IRandomizable _randomizeService;
        private readonly LevelsData _levelsData;
        private readonly string _bundleUrl;

        public Services(float screenHeight, float screenWidth, BackgroundDownloader backgroundDownloader, 
            BackgroundContainer backgroundContainer, BackgroundManager backgroundManager, Spawner spawner, LevelsData levelsData, string bundleUrl)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _spawner = spawner;
            _backgroundDownloader = backgroundDownloader;
            _backgroundContainer = backgroundContainer;
            _backgroundManager = backgroundManager;
            _levelsData = levelsData;
            _bundleUrl = bundleUrl;
        }

        public void InitRandomizer(LevelsData levelsData)
        {
            var currentLevelAndScore = ProgressTracker.GetCurrentLevelAndScore();
            
            var randomizeParameters = new RandomizeParameters
            {
                MinInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMinimum,
                MaxInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMaximum,
                MinSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMinimum,
                MaxSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMaximum
            };
            
            _randomizeService = new Randomizer(_screenHeight, _screenWidth, randomizeParameters);
        }

        private async Task<List<SpriteRenderer>> InitBackgrounds(string bundleUrl)
        {
            _backgroundDownloader.Init(_screenHeight, _screenWidth);
            
            var backgrounds = await _backgroundDownloader.DownloadBackgrounds(bundleUrl);
            
            foreach (var background in backgrounds) 
                _backgroundContainer.AddBackground(background);
            
            _backgroundManager.Init(_backgroundContainer);
            return backgrounds;
        }

        public async void StartGame()
        {
            InitRandomizer(_levelsData);
            await InitBackgrounds(_bundleUrl);
            _spawner.Init(_backgroundManager, _randomizeService, this);
        }
    }
}
