using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementController : MonoBehaviour
{
    public PlayerInput playerInput;
    public Rigidbody2D rb;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private Vector2 moveInput;

    public void MoveInput(InputAction.CallbackContext context)
    {
        Debug.Log("Move Input Detected");

        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();
            Move();
        }
    }

    public void Move()
    {
        Vector2 direction = SnapTo8Directions(moveInput);

        rb.linearVelocity = direction * moveSpeed;

        // Animator bool: true if moving, false if idle
        animator.SetBool("isRunning", rb.linearVelocity.sqrMagnitude > 0.01f);

        if (direction.x > 0.01f)
        {
            spriteRenderer.flipX = false; // facing right
        }
        else if (direction.x < -0.01f)
        {
            spriteRenderer.flipX = true; // facing left
        }
    }

    Vector2 SnapTo8Directions(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01f)
            return Vector2.zero;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;

        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
