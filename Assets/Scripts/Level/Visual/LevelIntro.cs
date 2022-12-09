using System.Collections;
using Data;
using Infrastructure;
using TMPro;
using UnityEngine;

namespace Level.Visual
{
    public class LevelIntro : MonoBehaviour
    {
        [SerializeField] private GameObject info;
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private float showTime;
        [SerializeField] private Spawner spawner;
        [SerializeField] private LevelsData levelsData;


        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(showTime);
            spawner.OnLevelChanged += ShowLevelNumber;
        }

        private void OnEnable()
        {
            var currentLevel = ProgressTracker.GetCurrentLevelAndScore().currentLevel;
            var levelName = levelsData.levels[currentLevel].name;
            StartCoroutine(SetAndShowInfo(levelName));
        }

        private IEnumerator SetAndShowInfo(string levelName)
        {
            //int currentScene = SceneManager.GetActiveScene().buildIndex;
            levelNumberText.text = levelName;
            info.SetActive(true);
            yield return _wait;
            info.SetActive(false);
        }

        private void ShowLevelNumber(string levelName)
        {
            StartCoroutine(SetAndShowInfo(levelName));
        }
    }
}
