using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winImage;
    public GameObject loseImage;
    
    [Header("Game Objects")]
    public GameObject player;
    public GameObject[] bears;
    
    private bool levelComplete = false;
    private bool playerDied = false;

    void Start()
    {
        InitializeUI();
    }

    void OnEnable()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        if (winImage != null)
            winImage.SetActive(false);
            
        if (loseImage != null)
            loseImage.SetActive(false);
            
        levelComplete = false;
        playerDied = false;
    }

    void Update()
    {
        if (!playerDied && player == null)
        {
            playerDied = true;
            HandlePlayerDeath();
        }
        string currentScene = SceneManager.GetActiveScene().name;

        if (!levelComplete && !playerDied && AllBearsDead() && currentScene != "Level3")
        {
            levelComplete = true;
            HandleLevelWin();
        }
    }

    bool AllBearsDead()
    {
        if (bears == null || bears.Length == 0)
            return false;

        foreach (var bear in bears)
        {
            if (bear != null)
                return false;
        }
        return true;
    }

    void HandlePlayerDeath()
    {
        if (loseImage != null)
            loseImage.SetActive(true);
            
        StartCoroutine(RestartLevel());
    }

    void HandleLevelWin()
    {
        if (winImage != null)
            winImage.SetActive(true);
            
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        
        if (loseImage != null)
            loseImage.SetActive(false);
            
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);
        
        if (winImage != null)
            winImage.SetActive(false);
        
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (currentScene == "Level2")
        {
            Debug.Log("All levels finished! Level3...");
            SceneManager.LoadScene("Level3");
        }
    }
}