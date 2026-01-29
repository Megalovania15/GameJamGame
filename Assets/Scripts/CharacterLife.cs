using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLife : MonoBehaviour, IMortal
{
    public GameObject characterBody;
    public GameObject bloodPuddle;

    public UnityEvent <DeathType, GameObject> OnDeath;

    public void Die(DeathType deathType)
    {
        switch (deathType)
        {
            case DeathType.Fall:
                Debug.Log($"{gameObject.name} died from falling.");
                break;
            case DeathType.Default:
                Instantiate(characterBody, transform.position, Quaternion.identity);
                Instantiate(bloodPuddle, transform.position, Quaternion.identity);
                break;
        }

        OnDeath.Invoke(deathType, gameObject);
        //gameObject.SetActive(false);
    }
}
