using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4f;
    public float gravity = -9.81f;
    public int maxJumps = 2;

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpsRemaining;
    private bool moveLeft, moveRight, isJumping;

    public Vector3 Velocity => velocity;
    public bool IsGrounded => controller.isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        if (!controller.enabled) return;

        HandleMovement();
        ApplyGravity();
        Jump();
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (moveLeft)
            velocity.x = -moveSpeed;
        else if (moveRight)
            velocity.x = moveSpeed;
        else
            velocity.x = 0;

        velocity.x = Mathf.Clamp(velocity.x, -moveSpeed, moveSpeed);
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
            velocity.y += gravity * Time.deltaTime;
        else
        {
            velocity.y = -0.5f;
            jumpsRemaining = maxJumps;
        }

        velocity.y = Mathf.Clamp(velocity.y, -50f, 50f);
    }

    private void Jump()
    {
        if (isJumping && jumpsRemaining > 0)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpsRemaining--;
            isJumping = false;
        }
    }

    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;
    public void OnJumpButtonDown()
    {
        if (jumpsRemaining > 0 && !isJumping)
            isJumping = true;
    }

    public void ResetMovement()
    {
        velocity = Vector3.zero;
        jumpsRemaining = maxJumps;
    }
}
