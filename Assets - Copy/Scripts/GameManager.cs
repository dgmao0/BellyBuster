using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject faboPrefab; 
    public GameObject janicePrefab; 

    void Start()
    {
        // Deactivate any existing characters in the scene
        DeactivateAllCharacters();

        // Determine camera size
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize * 2; // Height in world units
        float cameraWidth = cameraHeight * camera.aspect; // Width based on aspect ratio

        // Adjust these values based on your character's size
        float characterWidthOffset = 0.5f; // Half the width of your character
        float characterHeightOffset = 0.5f; // Half the height of your character

        // Calculate the bottom right corner position with offsets
        Vector3 spawnPosition = new Vector3((cameraWidth / 2) - characterWidthOffset, (-cameraHeight / 2) + characterHeightOffset, 0); // Adjust Y for bottom position

        // Check which character was selected and instantiate the corresponding prefab
        if (MainMenu.selectedCharacter == "Fabo")
        {
            Instantiate(faboPrefab, spawnPosition, Quaternion.identity); // Instantiate Fabo
        }
        else if (MainMenu.selectedCharacter == "Janice")
        {
            Instantiate(janicePrefab, spawnPosition, Quaternion.identity); // Instantiate Janice
        }
    }

    void DeactivateAllCharacters()
    {
        // Find and deactivate any GameObjects tagged with "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.SetActive(false); // Deactivate existing players
        }
    }
}