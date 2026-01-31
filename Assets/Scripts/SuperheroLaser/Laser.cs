using UnityEngine;

public class Laser : MonoBehaviour
{
    public float lifetime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,lifetime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterController player))
        {
            Debug.Log($"Laser hit {player.name}");
            //Do damage to that player
        }
    }

}
