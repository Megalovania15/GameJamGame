using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLife : MonoBehaviour, IMortal
{
    public GameObject characterBody;
    public GameObject bloodPuddle;

    [SerializeField]
    private UnityEvent OnDeath;

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

        OnDeath.Invoke();
        //gameObject.SetActive(false);
    }

    public void AddOnDeathListener(UnityAction action)
    {
        OnDeath.AddListener(action);
    }
}
