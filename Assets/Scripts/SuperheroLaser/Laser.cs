using Unity.Cinemachine;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float lifetime = 2f;

    public GameObject AbilityOwner { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,lifetime);
    }

    public void SetOwner(GameObject abilityOwner)
    {
        AbilityOwner = abilityOwner;
        Debug.Log(AbilityOwner.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMortal mortal))
        {
            if (collision.gameObject != AbilityOwner)
            {
                mortal.Die(DeathType.Default);
                AbilityOwner?.GetComponent<PlayerScore>().IncreaseKills();
            }

            //Debug.Log($"Laser hit {player.name}");
            //Do damage to that player
        }
    }

}
