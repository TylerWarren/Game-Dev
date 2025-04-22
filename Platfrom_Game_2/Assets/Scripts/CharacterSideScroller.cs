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
    private float jumpVelocity;

    public Vector3 Velocity => velocity;
    public bool IsGrounded => controller.isGrounded;

    private void Start()
    {
        Application.targetFrameRate = 60;
        controller = GetComponent<CharacterController>();
        jumpsRemaining = maxJumps;
        jumpVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    private void FixedUpdate()
    {
        if (!controller.enabled) return;

        float deltaTime = Time.fixedDeltaTime;

        // Movement
        velocity.x = moveLeft ? -moveSpeed : moveRight ? moveSpeed : 0;
        velocity.x = Mathf.Clamp(velocity.x, -moveSpeed, moveSpeed);

        // Gravity
        if (!controller.isGrounded)
            velocity.y += gravity * deltaTime;
        else
        {
            velocity.y = -0.5f;
            jumpsRemaining = maxJumps;
        }
        velocity.y = Mathf.Clamp(velocity.y, -50f, 50f);

        // Jump
        if (isJumping && jumpsRemaining > 0)
        {
            velocity.y = jumpVelocity;
            jumpsRemaining--;
            isJumping = false;
        }

        controller.Move(velocity * deltaTime);
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
