using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Reference to the Renderer component
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();
        
        // Check if the Renderer has a material
        if (objectRenderer != null && objectRenderer.material != null)
        {
            // Change the material color to red
            objectRenderer.material.color = Color.red;
        }
    }
}
