using UnityEngine;
using UnityEngine.Events;

public class MyTriggerEvents : MonoBehaviour
{
    public UnityEvent<Collider> triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        
        if (triggerEvent != null)
        {
            triggerEvent.Invoke(other);
        }
        else
        {
            Debug.LogWarning("triggerEvent is not assigned in " + gameObject.name);
        }
    }
}
