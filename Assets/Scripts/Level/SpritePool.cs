using System.Collections.Generic;
using Data;
using Textures;
using UnityEngine;

namespace Level
{
    public class SpritePool
    {
        public List<List<Sprite>> Sprites => _sprites;

        private readonly List<List<Sprite>> _sprites = new();
        
        public SpritePool(GameSettings gameSettings)
        {
            var textureGenerator = new TextureGenerator();

            for (int i = 0; i < gameSettings.spritePoolSettings.spriteSizes.Length; i++) 
                _sprites.Add(new List<Sprite>());

            for (int i = 0; i < gameSettings.spritePoolSettings.spriteSizes.Length; i++)
            {
                for (int j = 0; j < gameSettings.spritePoolSettings.eachSpriteAmount; j++)
                {
                    var color1 = GetRandomColor();
                    var color2 = GetRandomColor();

                    var resolution = gameSettings.spritePoolSettings.spriteSizes[i];
                
                    var sprite = textureGenerator.CreateSprite(gameSettings.spritePoolSettings.spriteSizes[i], 
                        gameSettings.spritePoolSettings.checkerSize, color1, color2, resolution);
                    _sprites[i].Add(sprite); 
                }
            }
        }

        public void ClearPool()
        {
            foreach (var spriteList in _sprites) 
                spriteList.Clear();
        }
        
        private Color GetRandomColor() => 
            new(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
