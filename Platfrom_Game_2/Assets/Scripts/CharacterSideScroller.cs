using UnityEngine;
using UnityEngine.UI; // For legacy UI Text
using System.Collections;

public class CharacterSideScroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4f;
    public float gravity = -9.81f;
    public int maxJumps = 2;
    public bool isInvulnerable = false;
    public float respawnInvulnerabilityTime = 2f;
    public float respawnDelay = 2f;

    [SerializeField] private Text respawnText; // Legacy UI Text
    [SerializeField] private GameManager gameManager;

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpsRemaining;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isJumping = false;

    // Checkpoint variables
    public Vector3 lastCheckpoint;
    public Vector3 initialPosition;

    private Renderer meshRenderer; // Reference to the player's mesh renderer

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        meshRenderer = GetComponent<Renderer>(); // Get the renderer at start
        if (meshRenderer == null)
        {
            Debug.LogError("[CharacterSideScroller] No Renderer found on player!");
        }
        jumpsRemaining = maxJumps;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (controller != null && controller.enabled)
        {
            HandleMovement();
            ApplyGravity();
            Jump();
            SetZPositionToZero();

            controller.Move(velocity * Time.deltaTime);

            // Existing fall condition
            if (transform.position.y < -10f)
            {
                Die();
            }
            // New condition: Check if mesh renderer is disabled (death trigger)
            else if (meshRenderer != null && !meshRenderer.enabled && !isInvulnerable)
            {
                Die();
            }
        }
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

    // Checkpoint methods
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        Debug.Log($"[Character] Checkpoint set to {checkpointPosition}");
    }

    public void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        Debug.Log($"[CharacterSideScroller] Player died. Waiting {respawnDelay} seconds before respawning...");

        if (controller != null)
        {
            controller.enabled = false;
        }

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false; // Ensure it's off (already should be)
        }

        if (respawnText != null)
        {
            respawnText.enabled = true;
        }

        yield return new WaitForSeconds(respawnDelay);

        if (respawnText != null)
        {
            respawnText.enabled = false;
        }

        if (controller != null)
        {
            controller.enabled = true;
        }
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true; // Re-enable renderer on respawn
        }

        Respawn();
    }

    public void Respawn()
    {
        Debug.Log($"[Respawn] Respawning to {lastCheckpoint}");

        if (controller != null)
        {
            controller.enabled = false;
            Vector3 adjustedPosition = lastCheckpoint + new Vector3(0, 0.5f, 0);
            transform.position = adjustedPosition;
            controller.enabled = true;
        }
        else
        {
            transform.position = lastCheckpoint;
        }

        velocity = Vector3.zero;
        jumpsRemaining = maxJumps;
        StartCoroutine(RespawnInvulnerability());
    }

    private IEnumerator RespawnInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(respawnInvulnerabilityTime);
        isInvulnerable = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isInvulnerable && hit.gameObject.CompareTag("Checkpoint"))
        {
            Vector3 checkpointPos = hit.transform.position;
            checkpointPos.z = 0;
            Debug.Log($"[CharacterSideScroller] Detected checkpoint at position: {checkpointPos}");
            SetCheckpoint(checkpointPos);
            if (gameManager != null)
            {
                gameManager.SaveGame();
            }
            else
            {
                Debug.LogWarning("[CharacterSideScroller] GameManager reference is null.");
            }
        }
    }

    // UI Button Methods
    public void OnLeftButtonDown() { moveLeft = true; }
    public void OnLeftButtonUp() { moveLeft = false; }
    public void OnRightButtonDown() { moveRight = true; }
    public void OnRightButtonUp() { moveRight = false; }
    public void OnJumpButtonDown() { isJumping = true; }
}