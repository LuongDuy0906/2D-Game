using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private float _bounceSpeed = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            AudioManager.Instance.PlayCoinSound();
            Destroy(collision.gameObject);
            GameManager.Instance.AddScore(1);
        } else if (collision.CompareTag("Trap"))
        {
            GameManager.Instance.GameOver();
        } else if (collision.CompareTag("Key"))
        {
            GameManager.Instance.GameWin();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                Vector2 contactNormal = collision.GetContact(i).normal;

                if(contactNormal.y > 0.7f)
                {
                    if(rb.linearVelocity.y < 0)
                    {
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, _bounceSpeed);
                        Destroy(collision.gameObject);
                        return;
                    }
                }
            }

            GameManager.Instance.GameOver();
        }
    }
}
