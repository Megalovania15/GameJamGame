using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public MaskData maskData;

    public Pickup _pickup;

    private void Start()
    {
        SetPickup(_pickup);
    }

    public void SetPickup(Pickup pickup)
    {
        maskData = pickup.maskData;
        spriteRenderer.sprite = pickup.maskPickupSprite;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        MaskEquip maskEquip = collision.GetComponent<MaskEquip>();
        if (maskEquip != null)
        {
            maskEquip.EquipMask(maskData);
            Destroy(this.gameObject);
        }
    }

}
