using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;

    public void UpdateCoinImages(int coin)
    {
        CoinImageUtil.UpdateImages(coinImages, coinSprite, coin);
    }
}
