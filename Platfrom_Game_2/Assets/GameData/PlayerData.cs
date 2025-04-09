using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/Player Data")]
public class PlayerData : ScriptableObject
{
    public int score; // Must be public for JsonUtility to serialize it

    public void Reset()
    {
        score = 0;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}