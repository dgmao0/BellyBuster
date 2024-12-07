using UnityEngine;

public class Sweet : MonoBehaviour
{
    // When the player collects the sweet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Sweet collected!");
            GameManager.Instance.CollectSweet(); 
            Destroy(gameObject); 
        }
    }
}
