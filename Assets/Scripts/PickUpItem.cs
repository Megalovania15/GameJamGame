using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public MaskData maskData;

    public Pickup _pickup;

    [SerializeField] private List<Pickup> pickups = new();

    [SerializeField] private bool respawns = false;
    
    [SerializeField] private float respawnTime = 5f;
    
    [SerializeField]
    private Sprite defaultSprite;

    private void Start()
    {
        // Kept for backward compatibility: prefer just using "pickups" list in inspector.
        if (_pickup is not null)
        {
            pickups.Add(_pickup);
        }
        SetPickup();
    }

    private void SetPickup()
    {
        var pickup = pickups[Random.Range(0, pickups.Count)];
        maskData = pickup.maskData;
        spriteRenderer.sprite = pickup.maskPickupSprite;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        MaskEquip maskEquip = collision.GetComponent<MaskEquip>();
        if (maskData != null && maskEquip != null)
        {
            maskEquip.EquipMask(maskData);
            maskData = null;
            spriteRenderer.sprite = defaultSprite;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            StartCoroutine(Respawn());
            // Destroy(this.gameObject);
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SetPickup();
    }
}
