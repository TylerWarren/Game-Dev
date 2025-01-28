using System;
using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;

    // This method is called when the ScriptableObject is loaded
    private void OnEnable()
    {
        value = 0; // Reset value to zero when the game starts
    }

    public void SetValue(int num)
    {
        value = num;
    }

    public void CompareValue(IntData obj)
    {
        if (value >= obj.value)
        {
            // No action needed
        }
        else
        {
            value = obj.value;
        }
    }

    public void SetValue(IntData obj)
    {
        value = obj.value;
    }

    public void UpdateValue(int num)
    {
        value += num;
    }
}