using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            audioManager.PlayCoinSound();
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        } else if (collision.CompareTag("Trap") || collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        } else if (collision.CompareTag("Key"))
        {
            gameManager.GameWin();
        }
    }
}
