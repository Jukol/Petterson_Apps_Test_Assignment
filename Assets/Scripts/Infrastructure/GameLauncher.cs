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
        
        [SerializeField] private string bundleUrlStandalone;
        [SerializeField] private string bundleUrlAndroid;
        [SerializeField] private string bundleUrlIos;
        
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

            var initParameters = SetInitParameters(screenHeight, screenWidth);

            _services = new Services(initParameters);
            
            _services.StartGame();
        }

        private InitializationParameters SetInitParameters(float screenHeight, float screenWidth)
        {
            var initParameters = new InitializationParameters();
            initParameters.ScreenHeight = screenHeight;
            initParameters.ScreenWidth = screenWidth;
            initParameters.BackgroundDownloader = backgroundDownloader;
            initParameters.BackgroundContainer = backgroundContainer;
            initParameters.BackgroundManager = backgroundManager;
            initParameters.Spawner = spawner;
            initParameters.LevelsData = levelsData;

            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.LinuxPlayer ||
                Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                initParameters.BundleUrl = bundleUrlStandalone;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                initParameters.BundleUrl = bundleUrlAndroid;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                initParameters.BundleUrl = bundleUrlIos;
            }

            return initParameters;
        }
    }
}
