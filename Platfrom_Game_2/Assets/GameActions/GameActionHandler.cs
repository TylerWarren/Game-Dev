using System.Collections;
using GameActions;
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction action;
    public UnityEvent startEvent, respondEvent, respondLateEvent;
    public float holdTime = 5.0f; // ⏳ Set delay to 5 seconds
    private WaitForSeconds waitObj;

    private void Awake()
    {
        waitObj = new WaitForSeconds(holdTime);
    }

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
    
    private IEnumerator RespondLate()
    {
        yield return waitObj;
        InvokeEvent(respondLateEvent);
    }

    private void OnDestroy()
    {
        if (action != null)
            action.RaiseNoArgs = null;
    }
    
    private void Respond()
    {
        InvokeEvent(respondEvent);

        if (!gameObject.activeInHierarchy) return;
    
        // Set custom respawn delay before starting coroutine
        SceneResetManager sceneResetManager = FindObjectOfType<SceneResetManager>();
        if (sceneResetManager != null)
        {
            sceneResetManager.SetRespawnDelay(holdTime);
        }

        StartCoroutine(RespondLate());
    }
}