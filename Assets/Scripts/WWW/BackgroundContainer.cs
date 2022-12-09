using System.Collections.Generic;
using UnityEngine;

public class BackgroundContainer : MonoBehaviour
{
    public List<SpriteRenderer> Backgrounds = new();

    public void AddBackground(SpriteRenderer background)
    {
        Backgrounds.Add(background);
    }
}
