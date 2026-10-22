using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<int> OnCoinChange;
    
    private int coin = 0;
    private bool isGameOver = false;
    private bool isGameWin = false;
    private float elapsedTime = 0f;

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
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene" || isGameOver || isGameWin) return;

        elapsedTime += Time.deltaTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnCoinChange?.Invoke(coin);
    }

    public void AddCoin(int count)
    {
        if(!isGameOver && !isGameWin)
        {
            coin += count;
            OnCoinChange?.Invoke(coin);
        }
    }

    public void GameOver()
    {   
        isGameOver = true;
        coin = 0;
        SetTimeScale(0);
        UIManager.Instance.ShowGameOver();
    } 

    public void GameWin()
    {
        isGameWin = true;
        SetTimeScale(0);
        UIManager.Instance.ShowWinGame(coin, elapsedTime);
    }

    public void RestartGame()
    {
        isGameOver = false;
        coin = 0;
        OnCoinChange?.Invoke(coin);
        SetTimeScale(1);
        LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        SetTimeScale(1);
    }

    public void PlayGame()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.allLevels[0]);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public bool IsGameOver()
    {
       return isGameOver; 
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    private void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    private void ResetGameState()
    {
        coin = 0;
        OnCoinChange?.Invoke(coin);

        isGameOver = false;
        isGameWin = false;
        SetTimeScale(1);
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResetGameState();
    }

}
