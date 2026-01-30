using UnityEngine;

public class DeathPit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IMortal>()?.Die(DeathType.Fall);
    }
}
