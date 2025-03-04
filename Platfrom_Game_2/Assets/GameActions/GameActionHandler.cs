using System.Collections;
using GameActions;
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction action;
    public UnityEvent startEvent, respondEvent; 

    private void Start()
    {
        InvokeEvent(startEvent);
    }

    private void OnEnable()
    {
        if (action != null)
            action.RaiseNoArgs += Respond;
    }

    private void InvokeEvent(UnityEvent unityEvent)
    {
        if (unityEvent != null)
        {
            unityEvent.Invoke();
        }
    }

    private void OnDisable()
    {
        if (action != null)
            action.RaiseNoArgs -= Respond;
    }

    private void OnDestroy()
    {
        if (action != null)
            action.RaiseNoArgs = null;
    }
    
    private void Respond()
    {
        InvokeEvent(respondEvent);
    }
}