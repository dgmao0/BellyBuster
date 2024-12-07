using UnityEngine;
using UnityEngine.SceneManagement;
public class Sweet : MonoBehaviour
{
    [SerializeField] private int cookiePoints = 20;
    [SerializeField] private int cakePoints = 40;
    [SerializeField] private AudioSource audioSource;

    

    private void Start()
    {
        // Grab the AudioSource attached to each instance (either cake or cookie prefab)
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the player’s score script
            PlayerScore playerScore = collision.GetComponent<PlayerScore>();

            if (playerScore != null)
            {
                if (gameObject.CompareTag("Cookie"))
                {
                    playerScore.AddPoints(cookiePoints);
                }
                else if (gameObject.CompareTag("Cake"))
                {
                    playerScore.AddPoints(cakePoints);
                }
                
                // Play the audio clip specific to each sweet
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                    Destroy(gameObject, audioSource.clip.length); // Delay destruction until after sound finishes
                }
                else
                {
                    Destroy(gameObject); // Immediate destruction if no sound is set
                }
            }
        }
    }
}