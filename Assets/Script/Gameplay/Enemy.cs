using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;

    private float startX;
    private Rigidbody2D rb;
    private bool movingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startX = rb.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movingRight && rb.position.x >= startX + distance)
        {
            Flip(false);
        } else if(!movingRight && rb.position.x <= startX - distance)
        {
            Flip(true);
        }

        rb.linearVelocity = new Vector2(
            movingRight ? speed : - speed, rb.linearVelocity.y
        );
    }

    void Flip(bool toRight)
    {
        if(movingRight == toRight) return;

        movingRight = toRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            Vector2 contactNormal = contact.normal;

            if(!movingRight && contactNormal.x > 0.7f)
            {
                Flip(true);
                break;
            } else if(movingRight && contactNormal.x < -0.7f)
            {
                Flip(false);
                break;
            }
        }
    }
}
