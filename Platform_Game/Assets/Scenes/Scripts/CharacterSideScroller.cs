using UnityEngine;

public class CharacterSideScroller : MonoBehaviour
{
    public CharacterConfig characterConfig; // Reference to the ScriptableObject

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpsRemaining;

    // Define class-level variables for movement properties
    private float moveSpeed;
    private float jumpForce;
    private float gravity; // Updated to use the gravity from CharacterConfig
    private int maxJumps;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // Initialize the character's properties using CharacterConfig if it exists
        if (characterConfig != null)
        {
            InitializeCharacterConfig();
        }

        // Set jumpsRemaining based on maxJumps from CharacterConfig
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // Call methods for character movement and behavior
        HorizontalMovement();
        ApplyGravity();
        Jump();
        SetZPositionToZero();

        // Apply all movement
        controller.Move(velocity * Time.deltaTime);
    }

    private void InitializeCharacterConfig()
    {
        // Use CharacterConfig values to set the character's properties
        moveSpeed = characterConfig.speed;
        jumpForce = characterConfig.jumpForce;
        gravity = characterConfig.gravity; // Use gravity from the config
        maxJumps = characterConfig.jump;
    }

    private void HorizontalMovement()
    {
        var moveInput = Input.GetAxis("Horizontal");
        var moveDirection = new Vector3(moveInput, 0f, 0f) * moveSpeed;
        velocity.x = moveDirection.x;
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity from the config
        }
        else
        {
            velocity.y = 0;
            jumpsRemaining = maxJumps; // Reset jumps when grounded
        }
    }

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump") || (!controller.isGrounded && jumpsRemaining <= 0)) return;

        velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        jumpsRemaining--;
    }

    private void SetZPositionToZero()
    {
        var position = transform.position;
        position.z = 0;
        transform.position = position;
    }
}