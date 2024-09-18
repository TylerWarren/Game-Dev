using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorIDDataList : ScriptableObject
{
    public List<ColorID> colorIDList;
    public ColorID currentColor;
    
    public void SetCurrentColorRandomly()
    {
        if (colorIDList == null || colorIDList.Count == 0)
        {
            Debug.LogWarning("ColorID list is empty or not set!");
            return;
        }
        
        int randomIndex = Random.Range(0, colorIDList.Count);
        currentColor = colorIDList[randomIndex];
    }
}