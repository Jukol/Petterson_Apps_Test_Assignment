using UnityEngine;
namespace Level
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Spawner spawner;

        private float _screenHeight;
        private float _screenWidth;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            ResizeByScreen();
            spawner.Init(_screenHeight, _screenWidth);
        }

        private void ResizeByScreen()
        {
            _screenHeight = _camera.orthographicSize * 2;
            _screenWidth = _screenHeight / Screen.height * Screen.width;

            Sprite sprite = spriteRenderer.sprite;
            Vector2 resizeFactor = new Vector2(_screenWidth / sprite.bounds.size.x, _screenHeight / sprite.bounds.size.y);

            Transform spriteTransform = spriteRenderer.transform;
            Vector2 spriteScale = spriteTransform.localScale;

            Vector2 newScale = new Vector2(spriteScale.x * resizeFactor.x, spriteScale.y * resizeFactor.y);

            spriteTransform.localScale = newScale;
        }
    }
}
