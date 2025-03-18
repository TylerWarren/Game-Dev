using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "GameData/Door Data")]
public class DoorData : ScriptableObject
{
    public bool isOpen;        // Tracks if the door is open
    public Vector3 position;   // Current position of the door
    
    // Reset to default values
    public void Reset()
    {
        isOpen = false;
        position = Vector3.zero;
    }
}