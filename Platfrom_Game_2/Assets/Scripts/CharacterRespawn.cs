using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterRespawn : MonoBehaviour
{
    public float respawnDelay = 2f;
    public float invulnerabilityTime = 2f;
    public Text respawnText;
    public GameManager gameManager;

    private CharacterController controller;
    private Renderer[] meshRenderers;
    private CharacterMovement movement;

    public Vector3 lastCheckpoint;
    public Vector3 initialPosition;
    private bool isInvulnerable;
    private bool isRespawning;

    private void Awake()
    {
        // Move controller fetching to Awake for earlier initialization
        controller = GetComponent<CharacterController>();
        if (!controller)
        {
            controller = GetComponentInChildren<CharacterController>();
            if (!controller)
            {
                Debug.LogWarning("CharacterController not found in Awake. Retrying in Start...");
            }
        }
    }

    private void Start()
    {
        // Retry fetching controller if not found in Awake
        if (!controller)
        {
            controller = GetComponent<CharacterController>();
            if (!controller)
            {
                controller = GetComponentInChildren<CharacterController>();
                if (!controller)
                {
                    Debug.LogError("CharacterController not found on " + gameObject.name + " or its children.");
                }
            }
        }

        meshRenderers = GetComponentsInChildren<Renderer>();
        movement = GetComponent<CharacterMovement>();

        initialPosition = transform.position;
        lastCheckpoint = initialPosition;

        // Validate components
        if (meshRenderers.Length == 0) Debug.LogWarning("No renderers found on player or children.");
        if (!movement) Debug.LogWarning("CharacterMovement not found.");
    }

    private void Update()
    {
        if (isRespawning || isInvulnerable) return;

        if (transform.position.y < -10f || AreRenderersDisabled())
        {
            Die();
        }
    }

    private bool AreRenderersDisabled()
    {
        foreach (var renderer in meshRenderers)
        {
            if (renderer && !renderer.enabled) return true;
        }
        return false;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        checkpointPosition.z = 0;
        lastCheckpoint = checkpointPosition;
        gameManager?.SaveGame();
        Debug.Log($"Checkpoint set at: {lastCheckpoint}");
    }

    public void Die()
    {
        if (isRespawning) return;
        isRespawning = true;
        StopAllCoroutines();
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        if (controller) controller.enabled = false;
        SetRenderersEnabled(false);
        if (respawnText) respawnText.enabled = true;

        yield return new WaitForSeconds(respawnDelay);

        if (respawnText) respawnText.enabled = false;
        SetRenderersEnabled(true);

        Respawn();
        isRespawning = false;
    }

    private void SetRenderersEnabled(bool enabled)
    {
        foreach (var renderer in meshRenderers)
        {
            if (renderer) renderer.enabled = enabled;
        }
    }

    public void Respawn()
    {
        if (!controller) return;

        Vector3 pos = lastCheckpoint != Vector3.zero ? lastCheckpoint : initialPosition;
        pos += new Vector3(0, 0.5f, 0);

        controller.enabled = false;
        transform.position = pos;
        controller.enabled = true;

        movement?.ResetMovement();
        StartCoroutine(InvulnerabilityCoroutine());
        Debug.Log($"Respawned at: {pos}");
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isInvulnerable && hit.gameObject.CompareTag("Checkpoint"))
        {
            SetCheckpoint(hit.transform.position);
        }
    }
}