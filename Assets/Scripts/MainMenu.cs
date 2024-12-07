using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject faboPrefab;
    public GameObject janicePrefab;

    // Select Fabo and load the cutscene
    public void SelectFabo()
    {
        PlayerData.Instance.selectedCharacter = faboPrefab; 
        PlayerPrefs.SetString("SelectedCharacter", "Fabo"); 
        SceneManager.LoadScene("Cutscene1");
    }

    // Select Janice and load the cutscene
    public void SelectJanice()
    {
        PlayerData.Instance.selectedCharacter = janicePrefab; 
        PlayerPrefs.SetString("SelectedCharacter", "Janice"); 
        Debug.Log($"Selected Character: {PlayerData.Instance.selectedCharacter.name}"); 
        SceneManager.LoadScene("Cutscene1"); 
    }
}
