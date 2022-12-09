using UnityEngine;
using UnityEngine.SceneManagement;
using WWW;

namespace Infrastructure
{
    public class Booter : MonoBehaviour
    {
        [SerializeField] private string bundleUrl;
        [SerializeField] private BackgroundDownloader backgroundDownloader;
        [SerializeField] private BackgroundContainer backgroundContainer;

        private async void Awake()
        {
            var backgrounds = await backgroundDownloader.DownloadBackgrounds(bundleUrl);

            foreach (var background in backgrounds) 
                backgroundContainer.AddBackground(background);

            LoadNextLevel();
            DontDestroyOnLoad(backgroundContainer);
        }

        private static void LoadNextLevel()
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentScene + 1);
        }
    }
}