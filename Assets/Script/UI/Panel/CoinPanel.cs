using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;

    private void SetAlpha(Image img, float alphaValue)
    {
        if(img == null) return;

        Color c = img.color;
        c.a = alphaValue;
        img.color = c;
    }

    public void UpdateCoinImages(int coin)
    {
        for(int i = 0; i < coinImages.Length; i++)
        {
            bool collected = i < coin;
            coinImages[i].sprite = collected ? coinSprite : null;
            SetAlpha(coinImages[i], collected ? 1f : 0f);
        }
    }
}
