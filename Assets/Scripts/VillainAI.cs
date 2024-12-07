using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VillainState
{
    Patrolling,
    Chasing
}

public class VillainAI : MonoBehaviour
{
    public VillainState currentState = VillainState.Patrolling;

    public Transform[] waypoints;
    public float speed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;

    private int currentWaypointIndex = 0;
    private Transform player;
    private bool isPlayerCaptured = false;

    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    void Update()
    {
        if (player == null || isPlayerCaptured) return;

        switch (currentState)
        {
            case VillainState.Patrolling:
                Patrol();
                break;
            case VillainState.Chasing:
                ChasePlayer();
                break;
        }

        DetectPlayer();
    }

    // Method for patrolling behavior
    void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        MoveTowards(targetWaypoint.position, speed);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    
    void ChasePlayer()
    {
        MoveTowards(player.position, chaseSpeed);
    }

    
    void MoveTowards(Vector3 target, float moveSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    
    void DetectPlayer()
    {
        if (GameManager.Instance == null) return;

        player = GameManager.Instance.GetPlayer()?.transform; 
        if (player == null) return;

        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        currentState = distanceToPlayer <= detectionRange ? VillainState.Chasing : VillainState.Patrolling;
    }

    
    IEnumerator FindPlayer()
    {
        while (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Player found: " + player.name);
            }
            else
            {
                Debug.Log("Waiting for player to spawn...");
            }
            yield return null;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerCaptured = true;

            
            GameManager.Instance.PlayerCaught();

            
            StartCoroutine(ResetVillainAfterCapture());
        }
    }

    
    IEnumerator ResetVillainAfterCapture()
    {
        yield return new WaitForSeconds(1f); 

        isPlayerCaptured = false; 
        currentState = VillainState.Patrolling; // Set state back to patrolling

        
        if (waypoints.Length > 0)
        {
            Transform closestWaypoint = waypoints[currentWaypointIndex];
            transform.position = closestWaypoint.position;
        }
    }
}
