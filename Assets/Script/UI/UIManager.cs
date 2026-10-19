using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;

    void Awake()
    {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        GameManager.OnScoreChange += HandleOnScoreChange;
    }

    void OnDisable()
    {
        GameManager.OnScoreChange -= HandleOnScoreChange;
    }

    void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    private void HandleOnScoreChange(int score)
    {
        UpdateScore(score);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void ShowWinGame()
    {
        gameWinUI.SetActive(true);
    }
}
