using Data;
using Level;
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

        private void Awake()
        {
            _camera = Camera.main;
            LaunchGame();
        }
        
        private void LaunchGame()
        {
            var screenHeight = _camera.orthographicSize * 2;
            var screenWidth = screenHeight / Screen.height * Screen.width;

            _services = new Services(screenHeight, screenWidth, backgroundDownloader, backgroundContainer, 
                                    backgroundManager, spawner, levelsData, bundleUrl);
            
            _services.StartGame();
        }
    }
}
