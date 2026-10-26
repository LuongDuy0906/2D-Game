using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanel : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private TextMeshProUGUI timeText;

    private void UpdateTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = $"{minutes}:{seconds}";
    }

    public void UpdateGameWinHub(int coinEarned, float time) 
    {
        CoinImageUtil.UpdateImages(coinImages, coinSprite, coinEarned);

        UpdateTimeText(time);
    }
}
