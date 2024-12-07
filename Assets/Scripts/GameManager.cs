using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject faboPrefab;
    public GameObject janicePrefab;
    public Transform defaultSpawnPoint; // Reference for player spawn point
    private GameObject currentPlayer;

    public int currentLevel = 1; // Tracking current level
    private bool isLevelTransitioning = false;

    public int playerLives = 3; // Player starts with 3 lives

    public bool allCakesAndCookiesCollected = false; // Track if player has collected all cakes and cookies

    private int totalSweets = 0; // Total number of sweets in the level
    private int collectedSweets = 0; // Number of sweets collected by the player

    public AudioSource soundEffectsSource; 
    public AudioClip sweetCollectSound; 
    public AudioSource backgroundMusicSource; 
    public AudioClip backgroundMusicClip; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        
        SceneManager.sceneLoaded += OnSceneLoaded; 

        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; 
            backgroundMusicSource.playOnAwake = true; 
            backgroundMusicSource.volume = 0.5f; 
        }

        if (!backgroundMusicSource.isPlaying && backgroundMusicClip != null)
        {
            backgroundMusicSource.Play(); 
        }
    }

    void Start()
    {
        // Only spawn player if it doesn't already exist
        if (currentPlayer == null)
        {
            SpawnPlayer();
        }

        
        totalSweets = GameObject.FindGameObjectsWithTag("Sweet").Length;
        Debug.Log("Total sweets in the scene: " + totalSweets);
    }

    void SpawnPlayer()
    {
        if (currentPlayer != null) return; 

       
        if (PlayerData.Instance == null || PlayerData.Instance.selectedCharacter == null)
        {
            Debug.LogError("No selected character found in PlayerData. Assigning a default character.");
            PlayerData.Instance.selectedCharacter = faboPrefab;
        }

        Debug.Log($"Selected Character: {PlayerData.Instance.selectedCharacter.name}");


        Vector3 spawnPosition = defaultSpawnPoint != null ? defaultSpawnPoint.position : Vector3.zero;

        if (PlayerData.Instance.selectedCharacter == faboPrefab)
        {
            currentPlayer = Instantiate(faboPrefab, spawnPosition, Quaternion.identity);
        }
        else if (PlayerData.Instance.selectedCharacter == janicePrefab)
        {
            currentPlayer = Instantiate(janicePrefab, spawnPosition, Quaternion.identity);
        }

        DontDestroyOnLoad(currentPlayer); 
        currentPlayer.tag = "Player";
        currentPlayer.SetActive(true);

        
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.FindPlayer();
        }
        Debug.Log($"Selected Character: {PlayerData.Instance.selectedCharacter.name}");

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (defaultSpawnPoint == null)
        {
            GameObject spawnPointObject = GameObject.FindWithTag("PlayerSpawnPoint");
            if (spawnPointObject != null)
            {
                defaultSpawnPoint = spawnPointObject.transform;
                Debug.Log("Default spawn point reassigned after scene load.");
            }
            else
            {
                Debug.LogError("Default spawn point could not be found in the new scene!");
            }
        }

        
        if (currentPlayer != null && defaultSpawnPoint != null)
        {
            currentPlayer.transform.position = defaultSpawnPoint.position;
            Debug.Log($"Player repositioned to {defaultSpawnPoint.position} in scene {scene.name}");
        }

       
        totalSweets = GameObject.FindGameObjectsWithTag("Sweet").Length;
        collectedSweets = 0; 
        Debug.Log("Total sweets in the new scene: " + totalSweets);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    public GameObject GetPlayer()
    {
        return currentPlayer;
    }

    
    public void PlayerCaught()
    {
        if (playerLives > 0)
        {
            playerLives--;
            Debug.Log("Lives remaining: " + playerLives);

            if (defaultSpawnPoint != null)
            {
                currentPlayer.transform.position = defaultSpawnPoint.position;
                Debug.Log("Player respawned at the start position.");
            }
            else
            {
                Debug.LogError("Default spawn point is not assigned! Player cannot respawn.");
            }

            if (playerLives == 0)
            {
                ShowLoserScene();
            }
        }
        else
        {
            Debug.Log("Player already has no lives left.");
        }
    }

    
    public void CollectSweet()
    {
        collectedSweets++;
        Debug.Log("Collected Sweets: " + collectedSweets);

        
        if (soundEffectsSource != null && sweetCollectSound != null)
        {
            soundEffectsSource.PlayOneShot(sweetCollectSound);
        }

        
        if (collectedSweets >= totalSweets)
        {
            allCakesAndCookiesCollected = true;
            Debug.Log("All cakes and cookies collected!");
            CheckLevelProgression(); 
        }
    }

    public void CheckLevelProgression()
    {
        
        if (allCakesAndCookiesCollected)
        {
            Debug.Log("All cakes and cookies collected. Progressing to next level.");
            
            
            if (isLevelTransitioning) 
            {
                Debug.Log("Level is transitioning, cannot progress yet.");
                return;
            }

            ProgressToNextLevel(); 
        }
    }

    public void ProgressToNextLevel()
    {
        
        string nextScene = "";

        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                nextScene = "Cutscene1";
                currentLevel = 1;
                break;
            case "Cutscene1":
                nextScene = "Level 1";
                break;
            case "Level 1":
                nextScene = "Cutscene2";
                break;
            case "Cutscene2":
                nextScene = "Level 2";
                break;
            case "Level 2":
                nextScene = "Cutscene3";
                break;
            case "Cutscene3":
                nextScene = "Level 3";
                break;
            case "Level 3":
                nextScene = "WinScene";
                if (currentPlayer != null)
            {
                Destroy(currentPlayer);
                currentPlayer = null;
                Debug.Log("Player destroyed before showing win scene.");
            }
                break;
            default:
                Debug.LogError("Invalid level or transition not defined.");
                break;
        }

        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("Next scene name is invalid or empty!");
            return;
        }
        Debug.Log($"Progressing to next level: {nextScene}");
        SceneManager.LoadScene(nextScene);
    }

    public void ShowLoserScene()
    {
        Debug.Log("Player lost. Showing loser scene.");
         if (currentPlayer != null)
    {
        Destroy(currentPlayer);
        currentPlayer = null;
        Debug.Log("Player destroyed before showing loser scene.");
    }
        SceneManager.LoadScene("LoserScene"); 
    }
}