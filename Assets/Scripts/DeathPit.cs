using UnityEngine;

public class DeathPit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IMortal>(out IMortal mortal))
        {
            mortal.Die(DeathType.Default);
        }
        
    }
}
