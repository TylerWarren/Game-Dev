using UnityEngine;
using GameActions;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private GameAction triggerAction; // Assign this in Inspector
    [SerializeField] private float raiseHeight = 2f;   // How high the spikes rise
    [SerializeField] private float raiseSpeed = 5f;    // Speed of rising animation
    [SerializeField] private float detectionRange = 3f;// Distance to trigger trap
    
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isTriggered = false;
    private GameObject player;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * raiseHeight;
        
        // Set up the action listener
        if (triggerAction != null)
        {
            triggerAction.Raise += OnPlayerDetected;
        }
    }

    private void FixedUpdate()
    {
        if (isTriggered && player != null)
        {
            // Move spikes up when triggered
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * raiseSpeed);
            
            // Reset if player moves out of range
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > detectionRange)
            {
                isTriggered = false;
            }
        }
        else
        {
            // Move spikes back down when not triggered
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * raiseSpeed);
        }
    }

    private void OnPlayerDetected(object obj)
    {
        if (obj is GameObject detectedObject && detectedObject.CompareTag("Player"))
        {
            player = detectedObject;
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                isTriggered = true;
            }
        }
    }

    private void OnDestroy()
    {
        if (triggerAction != null)
        {
            triggerAction.Raise -= OnPlayerDetected;
        }
    }
}