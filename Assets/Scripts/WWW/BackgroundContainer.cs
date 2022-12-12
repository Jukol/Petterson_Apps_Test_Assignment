using System.Collections.Generic;
using UnityEngine;

namespace WWW
{
    public class BackgroundContainer : MonoBehaviour
    {
        public List<SpriteRenderer> backgrounds = new();

        public void AddBackground(SpriteRenderer background)
        {
            backgrounds.Add(background);
        }
    }
}
