using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;

        private void Awake()
        {
            spawner.Init();
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