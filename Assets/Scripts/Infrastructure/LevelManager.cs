using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {
            LoadNextLevel();
            DontDestroyOnLoad(gameObject);
        }

        public static void LoadNextLevel()
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentScene + 1);
        }
    }
}