using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<int> OnScoreChange;
    
    private int score = 0;
    private bool isGameOver = false;
    private bool isGameWin = false;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnScoreChange?.Invoke(score);
    }

    public void AddScore(int point)
    {
        if(!isGameOver && !isGameWin)
        {
            score += point;
            OnScoreChange?.Invoke(score);
        }
    }


    public void GameOver()
    {   
        isGameOver = true;
        score = 0;
        SetTimeScale(0);
        UIManager.Instance.ShowGameOver();
    } 

    public void GameWin()
    {
        isGameWin = true;
        SetTimeScale(0);
        UIManager.Instance.ShowWinGame();
    }

    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        OnScoreChange?.Invoke(score);
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
        score = 0;
        OnScoreChange?.Invoke(score);

        isGameOver = false;
        isGameWin = false;
        SetTimeScale(1);
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResetGameState();
    }

}
