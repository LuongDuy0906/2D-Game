using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanel : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private TextMeshProUGUI timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void SetAlpha(Image img, float alpha) 
    {
        if(img == null) return;
        
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }

    private void UpdateTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = $"{minutes}:{seconds}";
    }

    public void UpdateGameWinHub(int coinEarned, float time) 
    {
        for(int i = 0; i < coinImages.Length; i++)
        {
            bool collected = i < coinEarned;
            coinImages[i].sprite = collected ? coinSprite : null;
            SetAlpha(coinImages[i], collected ? 1f : 0f);
        }

        UpdateTimeText(time);
    }
}
