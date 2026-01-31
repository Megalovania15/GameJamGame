using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLife : MonoBehaviour, IMortal
{
    public GameObject characterBody;
    public GameObject bloodPuddle;

    public Animator animator;
    public MaskEquip maskEquip;


    public SpriteRenderer body;
    public SpriteRenderer head;
    public SpriteRenderer mask;

    public CharacterDeathToggles deathToggles;

    [SerializeField]
    private UnityEvent OnDeath;

    public Transform[] spawnPoints;


    public void Start()
    {
        spawnPoints = PlayerManager.Instance.spawnPoints;
    }

    public void Die(DeathType deathType)
    {
        switch (deathType)
        {
            case DeathType.Fall:
                Debug.Log($"{gameObject.name} died from falling.");
                break;
            case DeathType.Default:
                //Instantiate(characterBody, transform.position, Quaternion.identity);
                SetDeathTrigger();
                Instantiate(bloodPuddle, transform.position, Quaternion.identity);
                maskEquip.UnequipMask();
                break;
        }

        OnDeath.Invoke();
        //gameObject.SetActive(false);



    }

    public void AddOnDeathListener(UnityAction action)
    {
        OnDeath.AddListener(action);
    }


    public void SetDeathTrigger()
    {
        animator.SetTrigger("death");
    }

    public void PlaceFakeBody()
    {
        Debug.Log("placing fake body");
        Instantiate(characterBody, transform.position, Quaternion.identity);
        body.enabled = false;
        head.enabled = false;
        mask.enabled = false;
        ResetPlayer();
    }


    public void ResetPlayer()
    {
        this.gameObject.transform.position = spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Length)].position;

        deathToggles.EnableCharacterComponents();
        animator.SetTrigger("reset");
    }

}
