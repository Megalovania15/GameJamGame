using UnityEngine;

public class CharacterDeathToggles : MonoBehaviour
{
    private CharacterController characterController;
    private Collider2D characterCollider;
    public SpriteRenderer bodySprite;
    public SpriteRenderer headSprite;
    public SpriteRenderer maskSprite;

    

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController>();
        characterCollider = GetComponent<Collider2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    //disables the following components on the character when an event occurs
    //need to unequip the mask when the player dies so they can't use it again
    //ideally they should drop the mask if it still has uses left
    public void DisableCharacterComponents()
    {
        rb.simulated = false;
        characterController.enabled = false;
        characterCollider.enabled = false;
        //sprite.enabled = false;
    }

    //reenables the character components, for example, when the player respawns.
    public void EnableCharacterComponents()
    {
        rb.simulated = true; 
        characterController.enabled = true;
        characterCollider.enabled = true;
        bodySprite.enabled = true;
        headSprite.enabled = true; 
        maskSprite.enabled = true;
    }
}
