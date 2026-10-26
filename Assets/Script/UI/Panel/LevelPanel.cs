using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private LevelData[] levels;
    [SerializeField] private Button levelButtonPrefab;
    [SerializeField] private Transform buttonContainer;

    private int currentPage = 0;
    
    private List<Button> activeButtons = new List<Button>();

    public void PopulateLevelButton()
    {
        foreach (var button in activeButtons)
        {
            Destroy(button.gameObject);
        }

        activeButtons.Clear();

        int start = currentPage * 6;
        int end = Mathf.Min(start + 6, levels.Length);

        for (int i = start; i < end; i++)
        {
            LevelData data = levels[i];
            int defaultUnlocked = data.levelNumber <= 1 ? 1 : 0;
            int isUnlocked = PlayerPrefs.GetInt($"{data.levelName}.isUnlocked", defaultUnlocked);
            int coinCollected = PlayerPrefs.GetInt($"{data.levelName}.coin", 0);
            Button levelButton = Instantiate(levelButtonPrefab, buttonContainer);
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = data.levelNumber.ToString();

            levelButton.onClick.AddListener(() => LevelManager.Instance.LoadLevel(data));
            levelButton.interactable = isUnlocked == 1;
            activeButtons.Add(levelButton);

            LevelButton buttonUI = levelButton.GetComponent<LevelButton>();

            buttonUI.ChangeCoinImage(coinCollected);
        }
    }

    public void NextPage()
    {
        if((currentPage + 1) * 6 >= levels.Length)
        {
            return;
        }

        currentPage++;

        PopulateLevelButton();
    }

    public void PreviousPage()
    {
        if(currentPage == 0)
        {
            return;
        }

        currentPage--;

        PopulateLevelButton();
    }
}
