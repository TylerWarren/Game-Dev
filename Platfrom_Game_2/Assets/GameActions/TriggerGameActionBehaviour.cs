using GameActions;
using UnityEngine;

public class TriggerGameActionBehaviour : MonoBehaviour
{
    [SerializeField] private GameAction action; // This can be KillAction

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            action?.RaiseAction(other.gameObject); // Pass player reference
        }
    }
}