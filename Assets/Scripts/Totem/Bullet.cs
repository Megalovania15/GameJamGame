using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float lifetime = 1f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = speed * transform.right;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IMortal>(out IMortal mortal))
        {
            mortal.Die(DeathType.Default);
        }

        Destroy(gameObject);
    }
}
