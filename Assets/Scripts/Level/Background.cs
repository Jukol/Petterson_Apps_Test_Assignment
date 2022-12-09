using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Level
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private string bundleUrl;
        private SpriteRenderer _spriteRenderer;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            DownloadSprite();
        }

        private async void DownloadSprite()
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl, 0);
            request.SendWebRequest();

            while (!request.isDone)
                await Task.Yield();
            
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject background = bundle.LoadAsset<GameObject>("BackgroundSprite_lvl1");
            var test = Instantiate(background);
            _spriteRenderer = test.GetComponent<SpriteRenderer>();
            ResizeByScreen();
        }

        private void ResizeByScreen()
        {
            var screenHeight = _camera.orthographicSize * 2;
            var screenWidth = screenHeight / Screen.height * Screen.width;

            Sprite sprite = _spriteRenderer.sprite;
            Vector2 resizeFactor = new Vector2(screenWidth / sprite.bounds.size.x, screenHeight / sprite.bounds.size.y);

            Transform spriteTransform = _spriteRenderer.transform;
            Vector2 spriteScale = spriteTransform.localScale;

            Vector2 newScale = new Vector2(spriteScale.x * resizeFactor.x, spriteScale.y * resizeFactor.y);

            spriteTransform.localScale = newScale;
        }
    }
}
