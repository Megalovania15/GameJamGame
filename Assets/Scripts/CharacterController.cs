using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    public MaskEquip maskEquip;

    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            maskEquip.ActivateAbilities();
        }
    }
}
