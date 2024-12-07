using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public float moveSpeed = 5f; // Speed of the character

    private Rigidbody2D rb;
    private Vector2 moveInput; 
    private Vector2 moveVelocity; 

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        moveInput.x = Input.GetAxis("Horizontal"); 
        moveInput.y = Input.GetAxis("Vertical");   

       
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
