using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameObject gameOverUI;
    private GameObject gameWinUI;
    private GameObject mobileUI;
    private TutorialPanelController tutorialPanelController;

    void Awake()
    {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameOverUI = gameUI.transform.Find("GameOverUI").gameObject;
        gameWinUI = gameUI.transform.Find("GameWinUI").gameObject;
        mobileUI = gameUI.transform.Find("MobileUI").gameObject;
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

    public void RegisterController(TutorialPanelController controller)
    {
        tutorialPanelController = controller;
    }

    public void UnregisterController(TutorialPanelController controller)
    {
        if(tutorialPanelController == controller)
        {
            tutorialPanelController = null;
        }
    }

    public void ShowTutorialPanel(TutorialType type)
    {
        if(tutorialPanelController == null) return;
        if(type == TutorialType.Coin) scoreText.gameObject.SetActive(true);
        tutorialPanelController.ShowTutorialPanel(type);
    }

    public void HideTutorialPanel(TutorialType type)
    {
        if(tutorialPanelController == null) return;
        tutorialPanelController.HideTutorialPanel(type);
    }

    public void LoadNextLevel()
    {
        var levelManager = LevelManager.Instance;
        int currentIndex = Array.IndexOf(levelManager.allLevels, levelManager.CurrentLevel);
        int nextIndex = currentIndex + 1;
        if(nextIndex < levelManager.allLevels.Length)
        {
            levelManager.LoadLevel(levelManager.allLevels[nextIndex]);
        }
    }

    private void ShowUI()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            menuUI.SetActive(true);
            gameUI.SetActive(false);
        } else if(SceneManager.GetActiveScene().name == "Tutorial_Level")
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
            scoreText.gameObject.SetActive(false);
        } else
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }

        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);

        mobileUI.SetActive(UnityEngine.Device.Application.isMobilePlatform);
    }
    
    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Canvas canvas = GetComponent<Canvas>();

        canvas.worldCamera = mainCamera;

        ShowUI();
    }
}
