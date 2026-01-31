using UnityEngine;

public class Beam : MonoBehaviour
{
    public GameObject AbilityOwner { get; private set; }

    public float lifetime = 2f;
    public int damage = 1;

    public float rotationSpeed = 180f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IMortal mortal))
        {
            if (other.gameObject != AbilityOwner)
            {
                //could maybe be switched to a flame death perhaps
                mortal.Die(DeathType.Default);
                AbilityOwner?.GetComponent<PlayerScore>().IncreaseKills();

                // Optional: ignore self
                //Debug.Log($"Beam hit {player.name}");
            }
        }
    }

    void Update()
    {
        // Clockwise rotation in 2D = negative Z
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    public void SetOwner(GameObject abilityOwner)
    {
        AbilityOwner = abilityOwner;
    }

}
