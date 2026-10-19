using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
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
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        GameManager.OnScoreChange += HandleOnScoreChange;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        GameManager.OnScoreChange -= HandleOnScoreChange;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
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

    private void ShowUI()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            menuUI.SetActive(true);
            gameUI.SetActive(false);
        } else
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }

        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }
    
    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Canvas canvas = GetComponent<Canvas>();

        canvas.worldCamera = mainCamera;

        ShowUI();
    }
}
