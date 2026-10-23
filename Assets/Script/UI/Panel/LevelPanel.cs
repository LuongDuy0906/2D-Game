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
            Button levelButton = Instantiate(levelButtonPrefab, buttonContainer);
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = data.levelNumber;

            levelButton.onClick.AddListener(() => LevelManager.Instance.LoadLevel(data));
            activeButtons.Add(levelButton);
        }
    }

    public void NextPage()
    {
        currentPage++;

        PopulateLevelButton();
    }

    public void PreviousPage()
    {
        currentPage--;

        PopulateLevelButton();
    }
}
