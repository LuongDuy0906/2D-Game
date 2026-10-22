using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanel : MonoBehaviour
{
    [SerializeField] private Image[] coinImages;
    [SerializeField] private Sprite coinSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < coinImages.Length; i++)
        {
            coinImages[i].sprite = null;
            SetAlpha(coinImages[i], 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetAlpha(Image img, float alpha) 
    {
        if(img == null) return;
        
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }
}
