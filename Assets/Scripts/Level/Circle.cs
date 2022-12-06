using UnityEngine;
namespace Level
{
    public class Circle : MonoBehaviour, ITargetable
    {
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed;
        [SerializeField] private int points;

        public void Init(Color color, float tSpeed, float size, Vector2 position, int points)
        {
            var myTransform = transform;
            myTransform.localScale = new Vector2(size, size);
            spriteRenderer.color = color;
            myTransform.position = position;
            speed = tSpeed;
            this.points = points;
        }

        private void Update()
        {
           MoveDown(speed);
        }

        public void MoveDown(float mSpeed)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * mSpeed));
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            Debug.Log("Points: " + points);
            Destroy(gameObject);
        }
    }
}
