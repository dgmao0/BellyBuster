using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score = 0;

    // Method to add points
    public void AddPoints(int points)
    {
        score += points;
        Debug.Log("Score: " + score); // Log the score to the console for testing
    }

    // Optional: Getter for score if needed by other scripts
    public int GetScore()
    {
        return score;
    }
}
