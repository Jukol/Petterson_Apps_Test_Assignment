using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize;
        

        private float _screenHeight;
        private float _screenWidth;

        public void Init(float height, float width)
        {
            _screenHeight = height;
            _screenWidth = width;
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                var randomData = Randomizer();
                var time = randomData.Time;
                var position = randomData.Position;
                var color = randomData.Color;
                var size = randomData.Size;
                var speed = 0.5f / size;
                int points = (int) (1 / size * 10);

                yield return new WaitForSeconds(time);
                
                circlePrefab.Init(color, speed, size, position, points);

                Instantiate(circlePrefab, transform);

                
            }
        }

        private (float Time, Vector2 Position, Color Color, float Size) Randomizer()
        {
            var time = Random.Range(minInterval, maxInterval);
            var size = Random.Range(minSize, maxSize);
            var position = new Vector2(Random.Range(-_screenWidth * 0.5f + size * 0.5f, _screenWidth * 0.5f - size * 0.5f), 
                                                    _screenHeight * 0.5f + size * 0.5f);
            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            

            return (time, position, color, size);
        }
    }
}
