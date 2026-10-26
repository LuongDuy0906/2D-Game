using UnityEngine;
using UnityEngine.UI;

public static class CoinImageUtil
{
    public static void UpdateImages(Image[] images, Sprite coinSprite, int coinCount)
    {
        for (int i = 0; i < images.Length; i++)
        {
            bool collected = i < coinCount;
            images[i].sprite = collected ? coinSprite : null;

            Color color = images[i].color;
            color.a = collected ? 1f : 0f;
            images[i].color = color;
        }
    }
}
