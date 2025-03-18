using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public int score;
    public List<string> collectedItems = new List<string>();

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}