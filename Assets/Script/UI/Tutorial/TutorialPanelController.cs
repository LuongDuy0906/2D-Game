using UnityEngine;

public class TutorialPanelController : MonoBehaviour
{
    [SerializeField] private GameObject movePanel;
    [SerializeField] private GameObject jumpPanel;
    [SerializeField] private GameObject stompPanel;
    [SerializeField] private GameObject coinPanel;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject informationPanel;

    void OnEnable()
    {
        UIManager.Instance.RegisterController(this);
    }

    void OnDisable()
    {
        UIManager.Instance.UnregisterController(this);
    }

    void Start()
    {
        HideAllPanel();
    }

    public void ShowTutorialPanel(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.Move:
                movePanel.SetActive(true);
                break;
            case TutorialType.Jump:
                jumpPanel.SetActive(true);
                break;
            case TutorialType.Stomp:
                stompPanel.SetActive(true);
                break;
            case TutorialType.Coin:
                coinPanel.SetActive(true);
                break;
            case TutorialType.Warning:
                warningPanel.SetActive(true);
                break;
            case TutorialType.Information:
                informationPanel.SetActive(true);
                break;
        }
    }

    public void HideTutorialPanel(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.Move:
                movePanel.SetActive(false);
                break;
            case TutorialType.Jump:
                jumpPanel.SetActive(false);
                break;
            case TutorialType.Stomp:
                stompPanel.SetActive(false);
                break;
            case TutorialType.Coin:
                coinPanel.SetActive(false);
                break;
            case TutorialType.Warning:
                warningPanel.SetActive(false);
                break;
            case TutorialType.Information:
                informationPanel.SetActive(false);
                break;
        }
    }

    public void HideAllPanel()
    {
        movePanel.SetActive(false);
        jumpPanel.SetActive(false);
        stompPanel.SetActive(false);
        coinPanel.SetActive(false);
        warningPanel.SetActive(false);
        informationPanel.SetActive(false);
    }
}
