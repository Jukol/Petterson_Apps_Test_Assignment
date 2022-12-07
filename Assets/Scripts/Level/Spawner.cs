using System;
using System.Collections;
using Level.Randomizer;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        public event Action<Circle> OnCircleCreated;
        [SerializeField] public bool running;
        
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize;

        private Camera _camera;
        private IRandomizable _randomizer;

        public void StartButtonHandler()
        {
            if (running) return;
            
            running = true;
            StartCoroutine(Spawn());
        }

        public void StopButtonHandler()
        {
            running = false;
            
            StopAllCoroutines();
            
            foreach (Transform child in transform) 
                Destroy(child.gameObject);
        }
        
        private void Awake() => 
            _camera = Camera.main;

        private void OnEnable() => 
            InitializeRandomizer();

        private IEnumerator Spawn()
        {
            while (true)
            {
                var randomData = _randomizer.GetRandomData();
                
                var time = randomData.Time;
                var place = randomData.Place;
                var color = randomData.Color;
                var size = randomData.Size;
                
                var speed = 0.5f / size;
                int points = (int) (1 / size * 10);
                
                yield return new WaitForSeconds(time);
                
                circlePrefab.Init(color, speed, size, place, points);
                var circle = Instantiate(circlePrefab, transform);
                
                OnCircleCreated?.Invoke(circle);
            }
        }
        
        private void InitializeRandomizer()
        {
            var screenHeight = _camera.orthographicSize * 2;
            var screenWidth = screenHeight / Screen.height * Screen.width;

            RandomizeParameters parameters = new RandomizeParameters
            {
                MinInterval = minInterval,
                MaxInterval = maxInterval,
                MinSize = minSize,
                MaxSize = maxSize
            };

            _randomizer = new Randomizer.Randomizer(screenHeight, screenWidth, parameters);
        }

        private void OnDisable()
        {
            OnCircleCreated = null;
        }
    }
}
