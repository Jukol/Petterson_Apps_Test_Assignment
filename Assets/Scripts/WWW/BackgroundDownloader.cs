using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using UnityEngine.Networking;

namespace WWW
{
    public class BackgroundDownloader : MonoBehaviour, IService
    {
        [SerializeField] private Transform backgroundContainer;
        [SerializeField] private float screenWidth;
        [SerializeField] private float screenHeight;

        public void Init(float fScreenHeight, float fScreenWidth)
        {
            screenHeight = fScreenHeight;
            screenWidth = fScreenWidth;
        }

        public async Task<List<SpriteRenderer>> DownloadBackgrounds(string ulr)
        {
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
            
            var request = UnityWebRequestAssetBundle.GetAssetBundle(ulr, 0);
            request.SendWebRequest();

            while (!request.isDone)
                await Task.Yield();
            
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject background = bundle.LoadAsset<GameObject>("Backgrounds");
            
            var downloadedGameObject = Instantiate(background, backgroundContainer);

            foreach (Transform child in downloadedGameObject.transform)
            {
                child.gameObject.SetActive(false);
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                
                if (sr != null)
                {
                    SpriteRenderer srResized = ResizeByScreen(sr);
                    spriteRenderers.Add(srResized);
                }
            }
            
            return spriteRenderers;
        }

        private SpriteRenderer ResizeByScreen(SpriteRenderer spriteRenderer)
        {
            Sprite sprite = spriteRenderer.sprite;
            Vector2 resizeFactor = new Vector2(screenWidth / sprite.bounds.size.x, screenHeight / sprite.bounds.size.y);

            Transform spriteTransform = spriteRenderer.transform;
            Vector2 spriteScale = spriteTransform.localScale;

            Vector2 newScale = new Vector2(spriteScale.x * resizeFactor.x, spriteScale.y * resizeFactor.y);

            spriteTransform.localScale = newScale;

            return spriteRenderer;
        }
    }
}
