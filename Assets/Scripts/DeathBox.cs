using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IMortal mortal))
        {
            mortal.Die(DeathType.Default);
        }
    }
}
