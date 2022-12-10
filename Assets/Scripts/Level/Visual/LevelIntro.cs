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
        private Services _services;
        private string _currentLevelName;

        public void Init(Services services)
        {
            _wait = new WaitForSeconds(showTime);
            _services = services;
            spawner.OnLevelChanged += ShowLevelName;
            
            var currentLevel = _services.GetCurrentLevelAndScore().currentLevel;
            _currentLevelName = levelsData.levels[currentLevel].name;
            StartCoroutine(SetAndShowInfo(_currentLevelName));
        }

        private IEnumerator SetAndShowInfo(string levelName)
        {
            levelNumberText.text = levelName;
            info.SetActive(true);
            yield return _wait;
            info.SetActive(false);
        }

        private void ShowLevelName(string levelName)
        {
            StartCoroutine(SetAndShowInfo(levelName));
        }
    }
}
