using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelData[] allLevels;
    public LevelData CurrentLevel { get; private set; }

    private int currentLevelIndex;
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        GameManager.OnGameWin += HandleGameWin;
    }

    void OnDisable()
    {
        GameManager.OnGameWin -= HandleGameWin;
    }

    public void LoadLevel(LevelData data)
    {
        CurrentLevel = data;
        SetCurrentLevelIndex(data);
        SceneManager.LoadScene(data.levelName);
    }

    private void HandleGameWin(int coin, float time)
    {
        PlayerPrefs.SetInt($"{CurrentLevel.levelName}.coin", coin);
        PlayerPrefs.SetFloat($"{CurrentLevel.levelName}.time", time);

        int nextIndex = currentLevelIndex + 1;

        if (currentLevelIndex >= 0 && nextIndex < allLevels.Length)
        {
            LevelData nextLevel = allLevels[nextIndex];
            PlayerPrefs.SetInt($"{nextLevel.levelName}.isUnlocked", 1);
        }

        PlayerPrefs.Save();
    }

    private void SetCurrentLevelIndex(LevelData data)
    {
        currentLevelIndex = Array.IndexOf(allLevels, data);
    }
}
