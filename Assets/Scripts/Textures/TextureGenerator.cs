using UnityEngine;
namespace Textures
{
    public class TextureGenerator
    {
        public Sprite CreateSprite(int texSize, int checkerSize, Color clr1, Color clr2, float resolution)
        {
            var squareTexture = CreateSquareTexture(texSize, checkerSize, clr1, clr2);

            var circledTexture = CalculateCircleTexture(squareTexture.height, squareTexture.width, 
                squareTexture.width * 0.5f, squareTexture.width * 0.5f, 
                squareTexture.width * 0.5f, squareTexture);
            
            return Sprite.Create(circledTexture, new Rect(0f, 0f, circledTexture.width, circledTexture.height), 
                new Vector2(0.5f, 0.5f), resolution);
        }

        private Texture2D CreateSquareTexture(int texSize, int checkerSize, Color color1, Color color2)
        {
            Texture2D squareTexture = new Texture2D(texSize, texSize, TextureFormat.RGBA32, false, false);
            
            for (int y = 0; y < squareTexture.height; y++)
            {
                for (int x = 0; x < squareTexture.width; x++)
                {
                    int checkerX = (x / checkerSize) % 2;
                    int checkerY = (y / checkerSize) % 2;

                    if ((checkerX == 0 && checkerY == 0) || (checkerX != 0 && checkerY != 0))
                        squareTexture.SetPixel(x, y, color1);
                    else
                        squareTexture.SetPixel(x, y, color2);
                }
            }
            
            squareTexture.Apply();

            return squareTexture;
        }
        
        private Texture2D CalculateCircleTexture(int height, int width, float radius, float xCoord, float yCoord, Texture2D sourceTex)
        {
            Color[] colors = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
            Texture2D texture2D = new Texture2D(height,width);
            
            for (int i = 0 ; i < height * width; i++)
            {
                int y = Mathf.FloorToInt(i / (float) width);
                int x = Mathf.FloorToInt(i - (float)(y * width));
                if (radius * radius >= (x - xCoord) * (x - xCoord) + (y - yCoord) * (y - yCoord))
                    texture2D.SetPixel(x, y, colors[i]);
                else
                    texture2D.SetPixel(x, y, Color.clear);
            }
            
            texture2D.Apply ();
            
            return texture2D;
        }
    }
    
    
}
