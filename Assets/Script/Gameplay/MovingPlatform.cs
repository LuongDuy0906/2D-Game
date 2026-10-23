using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;

    private Vector3 target;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            if(target == pointA.position)
            {
                target = pointB.position;
            } else
            {
                target = pointA.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                Vector2 contactNormal = collision.GetContact(i).normal;

                if(contactNormal.y < -0.7f)
                {
                    collision.transform.SetParent(transform);
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.transform.parent == transform
            && gameObject.activeInHierarchy
            && collision.gameObject.activeInHierarchy)
        {
            collision.transform.SetParent(null);
        }
    }
}
