using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreChange;
    
    private int score = 0;
    private bool isGameOver = false;
    private bool isGameWin = false;
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
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        SetTimeScale(1);
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
}
