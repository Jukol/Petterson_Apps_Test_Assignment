using UnityEngine;
namespace Level
{
    public class Circle : MonoBehaviour, ITargetable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed;

        private bool _canMove;

        public void Init(Color color, float tSpeed, float size, Vector2 position)
        {
            var myTransform = transform;
            myTransform.localScale = new Vector2(size, size);
            spriteRenderer.color = color;
            myTransform.position = position;
            speed = tSpeed;
        }

        private void Update()
        {
           
                MoveDown(speed);
        }

        public void MoveDown(float mSpeed)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * mSpeed));
        }
    }
}
