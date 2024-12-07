using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public float moveSpeed = 5f; // Speed of the character

    private Rigidbody2D rb;
    private Vector2 moveInput; // Input for movement
    private Vector2 moveVelocity; // Velocity based on input

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from WASD keys
        moveInput.x = Input.GetAxis("Horizontal"); // A/D keys
        moveInput.y = Input.GetAxis("Vertical");   // W/S keys

        // Calculate velocity based on input
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
