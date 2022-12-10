using Infrastructure;
using Level.Visual;
using UnityEngine;

namespace Level
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private GameObject curtain;
        [SerializeField] private HudManager hudManager;
        
        private BackgroundContainer _backgroundContainer;
        private Services _services;

        public void Init(BackgroundContainer backgroundContainer, Services services)
        {
            _backgroundContainer = backgroundContainer;
            _services = services;
        }

        public void MakeActiveBackground(int level)
        {
            foreach (var background in _backgroundContainer.Backgrounds) 
                background.gameObject.SetActive(false);
            
            _backgroundContainer.Backgrounds[level].gameObject.SetActive(true);
            hudManager.Init(_services);
            curtain.SetActive(false);
        }
    }
}
