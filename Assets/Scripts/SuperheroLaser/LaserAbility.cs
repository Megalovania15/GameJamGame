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

    public override void Activate(Vector2 direction)
    {
        if (laserActive) return;
        if (usesRemaining <= 0) return;

        if (prefab == null)
        {
            Debug.LogError("LaserAbility prefab is NULL");
            return;
        }

        if (eyeSocket == null) return;
        
        // Get laser rotation, rounded to nearest 8-direction cardinal.
        var laserAngle = Mathf.Atan2(direction.y, direction.x);
        laserAngle = Mathf.Round(laserAngle / (Mathf.PI / 4)) * (Mathf.PI / 4); 
        var laserRotation = Quaternion.Euler(0f, 0f, laserAngle * Mathf.Rad2Deg);
        
        // Spawn laser at eye position
        activeLaser = Instantiate(
            prefab,
            eyeSocket.position,
            laserRotation,
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
