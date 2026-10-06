using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        } else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
    }
}
