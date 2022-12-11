using System.Collections.Generic;
using Data;
using Textures;
using UnityEngine;
namespace Level
{
    public class SpritePool : MonoBehaviour
    {
        public List<List<Sprite>> Sprites => _sprites;

        [SerializeField] private List<Sprite> sprites0;
        [SerializeField] private List<Sprite> sprites1;
        [SerializeField] private List<Sprite> sprites2;
        [SerializeField] private List<Sprite> sprites3;

        private readonly List<List<Sprite>> _sprites = new();
        
        public void Init(GameSettings gameSettings)
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
            //Debug - To delete
            sprites0 = _sprites[0];
            sprites1 = _sprites[1];
            sprites2 = _sprites[2];
            sprites3 = _sprites[3];
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
