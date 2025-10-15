
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public GameObject mainMenu; // assign the MainMenu GameObject here

    private bool isMenuActive = true; // starts visible
    public float escapeten9ar=0;
    void Start()
    {
        // Ensure menu starts visible and game is paused
        if (mainMenu != null)
            mainMenu.SetActive(true);

        Time.timeScale = 0f; // pause game at start
    }

    void Update()
    {
        // Press ESC to toggle menu visibility
        if (Input.GetKey(KeyCode.Escape))
        {
            escapeten9ar+=1;
            ToggleMenu();
        }
    }

    public void StartGame()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);

        Time.timeScale = 1f; // resume game
        isMenuActive = false;
    }

    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;

        if (mainMenu != null)
            mainMenu.SetActive(isMenuActive);

        // Pause/unpause game
        Time.timeScale = isMenuActive ? 0f : 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Game quitting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}