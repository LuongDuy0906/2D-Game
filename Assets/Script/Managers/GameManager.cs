using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<int> OnCoinChange;
    public static event Action<int, float> OnGameWin;
    
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

        PauseGame();
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
        if(isGameOver || isGameWin) return;

        isGameOver = true;
        coin = 0;
        SetTimeScale(0);
        UIManager.Instance.ShowGameOver();
    } 

    public void GameWin()
    {
        if(isGameOver || isGameWin) return;

        isGameWin = true;
        SetTimeScale(0);
        OnGameWin?.Invoke(coin, elapsedTime);
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

    private void PauseGame()
    {
        if(Keyboard.current == null) return;

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UIManager.Instance.TogglePause();
        }
    }

    public bool IsGameOver()
    {
       return isGameOver; 
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    private void ResetGameState()
    {
        coin = 0;
        OnCoinChange?.Invoke(coin);

        isGameOver = false;
        isGameWin = false;
        elapsedTime = 0f;
        SetTimeScale(1);
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResetGameState();
    }

}
