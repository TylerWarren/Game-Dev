using UnityEngine;

[CreateAssetMenu]
public class MySo : ScriptableObject 
{   
    public float value;

    public void UpdateValue(float num)
    {
        value += num;
    }
}