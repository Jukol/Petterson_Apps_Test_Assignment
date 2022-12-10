using UnityEngine;

namespace Level
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private GameObject curtain;
        private BackgroundContainer _backgroundContainer;

        public void Init(BackgroundContainer backgroundContainer)
        {
            _backgroundContainer = backgroundContainer;
        }

        public void MakeActiveBackground(int level)
        {
            foreach (var background in _backgroundContainer.Backgrounds) 
                background.gameObject.SetActive(false);
            
            _backgroundContainer.Backgrounds[level].gameObject.SetActive(true);
            curtain.SetActive(false);
        }
    }
}
