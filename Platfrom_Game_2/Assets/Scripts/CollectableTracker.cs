using UnityEngine;
using UnityEngine.Events;

public class CollectableTracker : MonoBehaviour
{
    [Header("Collectible Tracking")]
    public int collectiblesCount = 0; // Tracks the number of collectibles

    [Header("Events")]
    public UnityEvent OnAllCollected; // Triggered when all collectibles are gathered

    private int collected = 0; // Tracks how many have been collected

    public void AddCollectible()
    {
        collectiblesCount++;
    }

    public void RemoveCollectible()
    {
        collected++;

        if (collected >= collectiblesCount && collectiblesCount > 0)
        {
            OnAllCollected?.Invoke();
        }
    }
}