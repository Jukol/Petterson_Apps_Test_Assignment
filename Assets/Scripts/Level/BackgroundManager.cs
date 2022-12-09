using UnityEngine;

namespace Level
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        
        private BackgroundContainer _backgroundContainer;

        private void Awake()
        {
            _backgroundContainer = FindObjectOfType<BackgroundContainer>();
            spawner.Init(this);
        }

        public void MakeActiveBackground(int level)
        {
            foreach (var background in _backgroundContainer.Backgrounds) 
                background.gameObject.SetActive(false);
            
            _backgroundContainer.Backgrounds[level].gameObject.SetActive(true);
        }
    }
}
