using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winImage;
    
    [Header("Game Objects")]
    public GameObject player;
    public GameObject[] bears;
    
    private bool levelComplete = false;

    void Start()
    {
        if (winImage != null)
            winImage.SetActive(false);
    }

    void Update()
    {
        if (player == null)
        {
            StartCoroutine(RestartLevel());
        }

        if (!levelComplete && AllBearsDead())
        {
            levelComplete = true;
            StartCoroutine(HandleLevelWin());
        }
    }

    bool AllBearsDead()
    {
        // Make sure we have bears to check
        if (bears == null || bears.Length == 0)
            return false;

        // Check if all bears are destroyed
        foreach (var bear in bears)
        {
            if (bear != null)
                return false;
        }
        return true;
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator HandleLevelWin()
    {
        if (winImage != null)
            winImage.SetActive(true);
        
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
            if (winImage != null)
                winImage.SetActive(true);
            SceneManager.LoadScene("Level1");
        }
    }
}