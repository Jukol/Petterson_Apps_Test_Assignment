using System;
using UnityEngine;

namespace Level.Targets
{
    public class Circle : MonoBehaviour, ITargetable
    {
        public event Action<int, int> OnTargetClicked;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed;
        [SerializeField] private int score;
        [SerializeField] private int maxScore;
        [SerializeField] private CircleCollider2D collider;
        

        public void Init(TargetInitParameters targetInitParameters)
        {
            Reset();
            
            var myTransform = transform;
            var size = targetInitParameters.Size;
            var position = targetInitParameters.Place;

            spriteRenderer.sprite = targetInitParameters.Sprite;

            myTransform.localScale = new Vector3(size, size, 1);
            myTransform.position = position;

            speed = targetInitParameters.Speed;
            score = targetInitParameters.Score;
            maxScore = targetInitParameters.MaxScore;
        }

        private void Update() => 
            MoveDown(speed);

        public void MoveDown(float mSpeed) => 
            transform.Translate(Vector3.down * (Time.deltaTime * mSpeed));

        private void Reset()
        {
            var myTransform = transform;
            
            myTransform.position = Vector2.zero;
            myTransform.localScale = Vector3.one;
            collider.radius = 0.5f;
        }

        private void OnBecameInvisible() => 
            Destroy(gameObject);

        private void OnMouseDown()
        {
            OnTargetClicked?.Invoke(score, maxScore);
            Destroy(gameObject);
        }

        private void OnDestroy() => 
            OnTargetClicked = null;
    }
}
