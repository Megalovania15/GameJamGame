using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CharacterController : MonoBehaviour
{
    public MaskEquip maskEquip;

    public Animator anim;

    public Vector2 steerDirection;

    public float currentZ = 0f;

    public float rotationSpeed = 200f;

    public float minZ = -40f;
    public float maxZ = 40f;

    public Transform eyeSocket;
    // FIXME: Extra spaghetti code to allow laser to be directed.
    // Rather merge character controller and movement controller.
    // They communicate with each other via the animator too T.T
    public CharacterMovementController movementController;

    private PlayerInput input;

    public void OnEnable()
    {
        PauseMenuController.OnPause.AddListener(SwitchActionMap);
        GameTimer.OnTimeUp.AddListener(SwitchActionMap);
    }

    public void OnDisable()
    {
        PauseMenuController.OnPause.RemoveListener(SwitchActionMap);
        GameTimer.OnTimeUp.RemoveListener(SwitchActionMap);
    }

    public void Awake()
    {
        movementController = GetComponent<CharacterMovementController>();
        input = GetComponent<PlayerInput>();
        input.uiInputModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
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

    public void SwitchActionMap(bool hasOpenUI)
    {
        //if is paused, this code runs. If the timer is activated, this doesn't run.
        if (!hasOpenUI) 
        {
            Debug.Log("Current action map is basic actions");
            input.SwitchCurrentActionMap("BasicActions");
        }
        else 
        {
            Debug.Log("Current action map is UI");
            input.SwitchCurrentActionMap("UI");
        }
    }

    /*public void SteerAction(InputAction.CallbackContext context)
    {
        steerDirection = context.ReadValue<Vector2>();
    }


    void LateUpdate()
    {
        float targetZ = steerDirection.x * maxZ;
        currentZ = Mathf.Lerp(currentZ, targetZ, 12f * Time.deltaTime);

        eyeSocket.localRotation = Quaternion.Euler(0f, 0f, currentZ);
    }*/





}
