using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private GameObject info;
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private float showTime;

    private WaitForSeconds _wait;

    private void Awake() => 
        _wait = new WaitForSeconds(showTime);

    private void OnEnable() => 
        StartCoroutine(SetAndShowInfo());

    private IEnumerator SetAndShowInfo()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        levelNumberText.text = currentScene.ToString();
        info.SetActive(true);
        yield return _wait;
        info.SetActive(false);
    }
}
