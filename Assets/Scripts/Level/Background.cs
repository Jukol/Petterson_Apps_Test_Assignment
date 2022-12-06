using UnityEngine;
namespace Level
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            ResizeByScreen();
        }

        private void ResizeByScreen()
        {
            float screenHeight = _camera.orthographicSize * 2;
            float screenWidth = screenHeight / Screen.height * Screen.width;

            Sprite sprite = spriteRenderer.sprite;
            Vector2 resizeFactor = new Vector2(screenWidth / sprite.bounds.size.x, screenHeight / sprite.bounds.size.y);

            Transform spriteTransform = spriteRenderer.transform;
            Vector2 spriteScale = spriteTransform.localScale;

            Vector2 newScale = new Vector2(spriteScale.x * resizeFactor.x, spriteScale.y * resizeFactor.y);

            spriteTransform.localScale = newScale;
        }
    }
}
