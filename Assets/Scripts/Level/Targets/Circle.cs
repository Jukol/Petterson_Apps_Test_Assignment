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

        public void Init(TargetInitParameters targetInitParameters)
        {
            var myTransform = transform;
            
            myTransform.localScale = new Vector2(targetInitParameters.Size, targetInitParameters.Size);
            spriteRenderer.color = targetInitParameters.Color;
            myTransform.position = targetInitParameters.Place;
            speed = targetInitParameters.Speed;
            score = targetInitParameters.Score;
            maxScore = targetInitParameters.MaxScore;
        }

        private void Update() => 
            MoveDown(speed);

        public void MoveDown(float mSpeed) => 
            transform.Translate(Vector3.down * (Time.deltaTime * mSpeed));

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
