using UnityEngine;

public class Beam : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 1;

    public float rotationSpeed = 180f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController player))
        {
            // Optional: ignore self
            Debug.Log($"Beam hit {player.name}");
        }
    }

    void Update()
    {
        // Clockwise rotation in 2D = negative Z
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

}
