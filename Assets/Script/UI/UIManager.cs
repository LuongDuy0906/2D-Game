using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;

    private GameObject gameOverUI;
    private GameWinPanel gameWinUI;
    private GameObject mobileUI;
    private CoinPanel coinPanel;
    private LevelPanel levelPanel;
    private GameObject mainMenuUI;
    private TutorialPanelController tutorialPanelController;
    private GameObject pausePanel;
    private bool isGamePause = false;

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
        gameWinUI = gameUI.transform.Find("GameWinUI").gameObject.GetComponent<GameWinPanel>();
        mobileUI = gameUI.transform.Find("MobileUI").gameObject;
        coinPanel = gameUI.transform.Find("GamePlayPanel").gameObject.transform.Find("CoinPanel").GetComponent<CoinPanel>();
        levelPanel = menuUI.transform.Find("LevelMenuPanel").gameObject.GetComponent<LevelPanel>();
        mainMenuUI = menuUI.transform.Find("MainMenuUI").gameObject;
        pausePanel = gameUI.transform.Find("PausePanel").gameObject;
    }

    void OnEnable()
    {
        GameManager.OnCoinChange += HandleOnCoinChange;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        GameManager.OnGameWin += HandleGameWin;
    }

    void OnDisable()
    {
        GameManager.OnCoinChange -= HandleOnCoinChange;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        GameManager.OnGameWin -= HandleGameWin;
    }

    private void HandleOnCoinChange(int coin)
    {
        UpdateCoin(coin);
    }

    private void UpdateCoin(int coin)
    {
        coinPanel.UpdateCoinImages(coin);
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void ShowWinGame(int coin, float time)
    {
        coinPanel.gameObject.SetActive(false);
        gameWinUI.UpdateGameWinHub(coin, time);
        gameWinUI.gameObject.SetActive(true);
    }

    public void ShowLevelMenu()
    {
        levelPanel.gameObject.SetActive(true);
        mainMenuUI.SetActive(false);
        levelPanel.PopulateLevelButton();
    }

    public void NextLevelMenuPage()
    {
        levelPanel.NextPage();
    }

    public void PreviousLevelMenuPage()
    {
        levelPanel.PreviousPage();
    }

    public void TogglePause()
    {
        if(GameManager.Instance.IsGameOver() || GameManager.Instance.IsGameWin())
        {
            return;
        }

        if(isGamePause)
        {
            isGamePause = false;
            pausePanel.SetActive(false);
            GameManager.Instance.SetTimeScale(1);
            coinPanel.gameObject.SetActive(true);
        } else
        {
            isGamePause = true;
            pausePanel.SetActive(true);
            GameManager.Instance.SetTimeScale(0);
            coinPanel.gameObject.SetActive(false);
        }
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
        if(type == TutorialType.Coin) coinPanel.gameObject.SetActive(true);
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
            mainMenuUI.SetActive(true);
            gameUI.SetActive(false);
        } else if(SceneManager.GetActiveScene().name == "Tutorial_Level")
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
            coinPanel.gameObject.SetActive(false);
        } else
        {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
            coinPanel.gameObject.SetActive(true);
        }

        isGamePause = false;

        gameOverUI.SetActive(false);
        gameWinUI.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        
        mobileUI.SetActive(UnityEngine.Device.Application.isMobilePlatform);
    }
    
    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Canvas canvas = GetComponent<Canvas>();

        canvas.worldCamera = mainCamera;

        ShowUI();
    }

    private void HandleGameWin(int coin, float time)
    {
        ShowWinGame(coin, time);
    }
}
