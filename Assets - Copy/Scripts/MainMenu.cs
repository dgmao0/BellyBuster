using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static string selectedCharacter; // what they chose(janice or fabo)

    public void SelectFabo()
    {
        selectedCharacter = "Fabo";
        StartGame();
    }

    public void SelectJanice()
    {
        selectedCharacter = "Janice";
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Debug log for testing in the editor
    }
}
