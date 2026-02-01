using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
