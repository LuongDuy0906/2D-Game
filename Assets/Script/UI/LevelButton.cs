using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;

    public void ChangeCoinImage(int coin)
    {
        CoinImageUtil.UpdateImages(coinImages, coinSprite, coin);
    }
}
