using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject villainPrefab;
    public Transform villainSpawnPoint;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button option1Button;
    public Button option2Button;

    private string selectedCharacter;
    private int cutsceneNumber; 

    void Start()
    {
        
        selectedCharacter = PlayerData.Instance.selectedCharacter.name;

        
        if (villainPrefab == null)
        {
            Debug.LogError("Villain prefab not set in the inspector!");
            return;
        }

        GameObject villain = Instantiate(villainPrefab, villainSpawnPoint.position, Quaternion.identity);

        
        option1Button.onClick.AddListener(() => ChooseOption(1));
        option2Button.onClick.AddListener(() => ChooseOption(2));

        
        cutsceneNumber = GetCutsceneNumber(); 
        StartDialogue();
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        if (cutsceneNumber == 1) 
        {
            dialogueText.text = "Villain: 'Hey! I can't let you get any more sugar, stop now and eat something healthy.'";
            SetOptions("Sorry but no! I need sugar!", "Just move out the way and let me eat my sweets!");
        }
        else if (cutsceneNumber == 2) 
        {
            dialogueText.text = "Villain: 'You just won't quit! I would stop now before you embarrass yourself.'";
            SetOptions("Just let me have my sweets!", "Ha! I don't care Doctor! Move so I can get to my cakes and cookies!");
        }
        else if (cutsceneNumber == 3) 
        {
            dialogueText.text = "Villain: 'I've had enough! This is your last chance to go eat something healthy!'";
            SetOptions("C'mon Doctor. I can't stop till my Belly Busts!!", "This is your last chance to get out of my way!");
        }
        else
        {
            Debug.LogError("Cutscene number not recognized.");
        }
    }

    void SetOptions(string option1, string option2)
    {
        option1Button.GetComponentInChildren<TMP_Text>().text = option1;
        option2Button.GetComponentInChildren<TMP_Text>().text = option2;
    }

    void ChooseOption(int option)
    {
        
        option1Button.interactable = false;
        option2Button.interactable = false;

        if (cutsceneNumber == 1) 
        {
            if (option == 1)
            {
                dialogueText.text = "Villain: 'I guess I must do it myself...'";
            }
            else
            {
                dialogueText.text = "Villain: 'Oh, I'm not letting you get a single cake!'";
            }
        }
        else if (cutsceneNumber == 2) 
        {
            if (option == 1)
            {
                dialogueText.text = "Villain: 'I can't do that! Here we go again...'";
            }
            else
            {
                dialogueText.text = "Villain: 'The nerve of you! I'll get you this time!'";
            }
        }
        else if (cutsceneNumber == 3) 
        {
            if (option == 1)
            {
                dialogueText.text = "Villain: 'It won't bust on my watch!'";
            }
            else
            {
                dialogueText.text = "Villain: 'Fine! Don't say I didn't warn you...'";
            }
        }

        
        Invoke(nameof(LoadNextLevel), 2f);
    }

    void LoadNextLevel()
    {
        
        dialoguePanel.SetActive(false);

        
        GameManager.Instance.ProgressToNextLevel();
    }

    
    int GetCutsceneNumber()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Cutscene1") return 1;
        else if (currentScene == "Cutscene2") return 2;
        else if (currentScene == "Cutscene3") return 3;
        else return 0;
    }
}
