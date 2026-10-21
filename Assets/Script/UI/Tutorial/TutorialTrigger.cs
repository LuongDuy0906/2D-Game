using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialType tutorialType;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowTutorialPanel(tutorialType);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.HideTutorialPanel(tutorialType);
        }
    }
}
