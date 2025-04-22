using UnityEngine;

[CreateAssetMenu(fileName = "IntData", menuName = "ScriptableObjects/IntData")]
public class IntData : ScriptableObject
{
    public int value;

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public void UpdateValue(int amount)
    {
        value += amount;
    }
    
    private void OnEnable()
    {
        value = 0;
    }
}   
