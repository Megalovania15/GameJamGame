using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    public MaskEquip maskEquip;

    public Animator anim;

    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            maskEquip.ActivateAbilities();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("attack");
        }
    }
    

}
