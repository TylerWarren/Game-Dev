using System.Collections;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public float holdTime = 0.1f;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(holdTime);
            
         
            if (other != null)
            {
                Destroy(other.gameObject); 
            }
        }
    }
}