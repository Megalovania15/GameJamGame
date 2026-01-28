using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMock : MonoBehaviour, IMortal
{
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = 1;
    }
    
    public void Die(DeathType deathType)
    {
        Debug.Log("I died from: " + deathType);
    }
}
