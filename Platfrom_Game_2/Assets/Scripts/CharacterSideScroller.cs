using UnityEngine;
using UnityEngine.UI;

public class CharacterSideScroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4f;
    public float gravity = -9.81f;
    public int maxJumps = 2;

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpsRemaining;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isJumping = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
        Jump();
        SetZPositionToZero();

        // Apply all movement
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (moveLeft)
        {
            velocity.x = -moveSpeed;
        }
        else if (moveRight)
        {
            velocity.x = moveSpeed;
        }
        else
        {
            velocity.x = 0;
        }
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
            jumpsRemaining = maxJumps;
        }
    }

    private void Jump()
    {
        if (!isJumping || (!controller.isGrounded && jumpsRemaining <= 0)) return;
        velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        jumpsRemaining--;
        isJumping = false;
    }

    private void SetZPositionToZero()
    {
        var transform1 = transform;
        var position = transform1.position;
        position.z = 0;
        transform1.position = position;
    }

    // UI Button Methods
    public void OnLeftButtonDown()
    {
        moveLeft = true;
    }

    public void OnLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnRightButtonDown()
    {
        moveRight = true;
    }

    public void OnRightButtonUp()
    {
        moveRight = false;
    }

    public void OnJumpButtonDown()
    {
        isJumping = true;
    }
}