using UnityEngine;
using UnityEngine.Events;

public class MyTriggerEvents : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public void OnTriggerEnter(Collider other)
    { 
        triggerEvent.Invoke();
    }
}
