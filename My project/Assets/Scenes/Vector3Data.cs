using UnityEngine;

public class Vector3Data : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

         
        Vector3 move = new Vector3(moveX, moveY, 0);

       
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}