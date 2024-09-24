using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CoroutineBehaviour : MonoBehaviour
{
    public UnityEvent startEvent, startCountEvent, repeatCountEvent, endCountEvent, repeatUntilFalseEvent;


    public bool canRun;
    public IntData counterNum;
    public float seconds = 3.0f;
    private WaitForSeconds wfsObj;


    public bool CanRun
    {
        get => canRun;
        set => canRun = value;
    }


    private void Start()
    {
        wfsObj = new WaitForSeconds(seconds);
        StartInstancing();
    }

    public void StartButton()
    {
        startEvent.Invoke();
        StartCounting();
        
    }


    public void StartCounting()
    {
        StartCoroutine(Counting());
    }

    private void StartInstancing()
    {
        canRun = true;
        StartCoroutine(RepeatUntilFalse());
    }

    private IEnumerator Counting()
    {
        startCountEvent.Invoke();
        yield return wfsObj;
        while (counterNum.value > 0)
        {
            repeatCountEvent.Invoke();
            counterNum.value--;
            yield return wfsObj;
        }

        endCountEvent.Invoke();
    }
    private IEnumerator RepeatUntilFalse()
    {
        while (canRun)
        {
            yield return wfsObj;
            repeatUntilFalseEvent.Invoke();
        }
    }
    public void StopInstancing()
    {
        canRun = false;
    }
}    
