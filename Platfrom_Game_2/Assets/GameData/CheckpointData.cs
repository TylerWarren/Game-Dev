using UnityEngine;
public class CheckpointData : ScriptableObject
{
    [SerializeField]
    private Vector3 checkpointPosition;
    [SerializeField]
    private bool hasCheckpointBeenSet = false;

    public Vector3 CheckpointPosition
    {
        get => checkpointPosition;
        set
        {
            checkpointPosition = value;
            hasCheckpointBeenSet = true;
        }
    }

    public bool HasCheckpointBeenSet => hasCheckpointBeenSet;

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpointBeenSet = true;
    }

    public void Reset()
    {
        checkpointPosition = Vector3.zero;
        hasCheckpointBeenSet = false;
    }
}