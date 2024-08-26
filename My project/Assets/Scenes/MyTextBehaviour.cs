using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MyTextBehaviour : MonoBehaviour
{
    public Text label;
    public MySo dataObj;
    
    private void Start()
    {
        label = GetComponent<Text>();
        UpdateLabel();
    }

    public void UpdateLabel()

    {
        label.text = dataObj.value.ToString(CultureInfo.InvariantCulture);
    }
}