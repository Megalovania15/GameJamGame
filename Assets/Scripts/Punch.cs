using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 8f;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag != "Player")
            return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            return;

        Vector2 direction = (collision.transform.position - transform.position).normalized;

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
