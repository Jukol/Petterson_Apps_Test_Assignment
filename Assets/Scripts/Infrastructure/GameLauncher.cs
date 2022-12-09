using System.Collections.Generic;
using Data;
using Level;
using Level.Randomizer;
using Unity.VisualScripting;
using UnityEngine;
using WWW;

namespace Infrastructure
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;
        [SerializeField] private BackgroundDownloader backgroundDownloader;
        [SerializeField] private Spawner spawner;
        [SerializeField] private string bundleUrl;
        [SerializeField] private BackgroundManager backgroundManager;
        [SerializeField] private BackgroundContainer backgroundContainer;
        

        private IServices _services; 
        private Camera _camera;
        private List<SpriteRenderer> _backgrounds;

        private void Awake()
        {
            _camera = Camera.main;
            LaunchGame();
        }
        
        private async void LaunchGame()
        {
            var screenHeight = _camera.orthographicSize * 2;
            var screenWidth = screenHeight / Screen.height * Screen.width;
            var currentLevelAndScore = ProgressTracker.GetCurrentLevelAndScore();
            
            var randomizeParameters = new RandomizeParameters
            {
                MinInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMinimum,
                MaxInterval = levelsData.levels[currentLevelAndScore.currentLevel].intervalMaximum,
                MinSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMinimum,
                MaxSize = levelsData.levels[currentLevelAndScore.currentLevel].sizeMaximum
            };

            backgroundDownloader.Init();
            _backgrounds = await backgroundDownloader.DownloadBackgrounds(bundleUrl);
            
            foreach (var background in _backgrounds) 
                backgroundContainer.AddBackground(background);
            
            backgroundManager.Init(backgroundContainer);

            _services = new Services(screenHeight, screenWidth, randomizeParameters, _backgrounds, spawner, backgroundManager);

            _services.StartGame();
        }
    }
}
