using System.Collections.Generic;
using Level;
using Level.Randomizer;
using UnityEngine;
namespace Infrastructure
{
    public class Services : IServices
    {
        private IService _randomizeService;
        private List<SpriteRenderer> _backgrounds;
        private Spawner _spawner;
        private BackgroundManager _backgroundManager;

        public Services(float screenHeight, float screenWidth, RandomizeParameters randomizeParameters, List<SpriteRenderer> backgrounds, Spawner spawner, BackgroundManager backgroundManager)
        {
            _randomizeService = new Randomizer(screenHeight, screenWidth, randomizeParameters);
            _backgrounds = backgrounds;
            _spawner = spawner;
            _backgroundManager = backgroundManager;
        }

        public void StartGame() => 
            _spawner.Init(_backgroundManager);
    }
}
