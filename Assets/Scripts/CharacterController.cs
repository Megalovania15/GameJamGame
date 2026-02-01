using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public MaskEquip maskEquip;

    public Animator anim;

    // FIXME: Extra spaghetti code to allow laser to be directed.
    // Rather merge character controller and movement controller.
    // They communicate with each other via the animator too T.T
    public CharacterMovementController movementController;

    public void Awake()
    {
        movementController = GetComponent<CharacterMovementController>();
    }

    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            maskEquip.ActivateAbilities(GetActionDirection());
        }
    }

    private Vector3 GetActionDirection()
    {
        var direction = movementController.LastValidInput.normalized;
        // Invert the direction in case the player is flipped:
        if (transform.localScale.x < 0f)
        {
            direction = -direction;
        }
        return direction;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("attack");
        }
    }
    

}
