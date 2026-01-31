using UnityEngine;

public class LaserAbility : ActiveAbility
{

    public float duration = 2f;
    public int maxUses = 2;

    public int usesRemaining;
    private float elapsedTime;
    private bool laserActive;

    private GameObject activeLaser;
    private Transform eyeSocket;

    void Start()
    {
        usesRemaining = maxUses;

        // Find eye socket on player
        eyeSocket = ownerTransform.Find("EyeSocket");

        if (eyeSocket == null)
        {
            Debug.LogError("LaserAbility: EyeSocket not found on player");
        }
    }

    public override void Activate()
    {
        if (laserActive) return;
        if (usesRemaining <= 0) return;

        if (prefab == null)
        {
            Debug.LogError("LaserAbility prefab is NULL");
            return;
        }

        if (eyeSocket == null) return;

        // Spawn laser at eye position
        activeLaser = Instantiate(
            prefab,
            eyeSocket.position,
            eyeSocket.rotation,
            eyeSocket
        );

        Debug.Log(activeLaser);

        activeLaser.GetComponentInChildren<Laser>().SetOwner(gameObject);
        Debug.Log("minus lasers!!");
        usesRemaining--;
        laserActive = true;
        elapsedTime = 0f;
        
    }

    private void Update()
    {
        if (!laserActive) return;

        elapsedTime += Time.deltaTime;

        // Keep laser locked to eyes
        if (activeLaser != null)
        {
            activeLaser.transform.position = eyeSocket.position;
            activeLaser.transform.rotation = eyeSocket.rotation;
        }

        if (elapsedTime >= duration)
        {
            EndLaser();
        }
    }

    private void EndLaser()
    {
        laserActive = false;

        if (activeLaser != null)
        {
            Destroy(activeLaser);
        }

        // Only unequip AFTER final use + laser finishes
        if (usesRemaining <= 0)
        {
            owner.GetComponent<MaskEquip>().UnequipMask();
        }
    }


}
