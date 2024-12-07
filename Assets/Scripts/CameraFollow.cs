using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset = new Vector3(0, 2, 0); // Camera offset (2 units above the player)

    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Dynamically find the player object
        FindPlayer();
    }

    void LateUpdate()
    {
        if (player != null)
        {

            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            
            transform.position = smoothedPosition;
        }
    }

    public void FindPlayer()
    {
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform; 
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure the instantiated character has the 'Player' tag.");
        }
    }
}

