using UnityEngine;
namespace Level.Randomizer
{
    public class Randomizer : IRandomizable
    {
        private readonly float _screenWidth;
        private readonly float _screenHeight;
        private readonly RandomizeParameters _parameters;

        public Randomizer(float screenHeight, float screenWidth, RandomizeParameters parameters)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _parameters = parameters;
        }
        
        public RandomData GetRandomData()
        {
            var randomData = new RandomData();
            
            randomData.Time = Random.Range(_parameters.MinInterval, _parameters.MaxInterval);
            randomData.Size = Random.Range(_parameters.MinSize, _parameters.MaxSizeFactor);

            var bounds = GetPositionBounds(randomData.Size);
            
            randomData.Place = new Vector2(Random.Range(bounds.LeftBound, bounds.RightBound), bounds.TopBound);

            return randomData;
        }

        private (float LeftBound, float RightBound, float TopBound) GetPositionBounds(float circleSize)
        {
            float leftBound = -_screenWidth * 0.5f + (circleSize * _screenWidth) * 0.5f;
            float rightBound = _screenWidth * 0.5f - (circleSize * _screenWidth) * 0.5f;
            float topBound = _screenHeight * 0.5f + (circleSize * _screenWidth) * 0.5f;

            return (leftBound, rightBound, topBound);
        }
    }
}
