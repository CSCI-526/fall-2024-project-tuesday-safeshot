using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton;
    public Button continueButton;  // Continue button reference
    public Button restartButton;   // Restart button reference
    public Button mainMenuButton;  // Main Menu button reference
    private bool isPaused = false;
    private bool gameOver = false;  // Flag to track if the game has ended

    private LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        pauseMenuUI.SetActive(false);

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePauseMenu);
        }
        else
        {
            Debug.LogError("Pause button is not assigned!");
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(TogglePauseMenu);  // Add listener for Continue button
        }
        else
        {
            Debug.LogError("Continue button is not assigned!");
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(Restart);  // Add listener for Restart button
        }
        else
        {
            Debug.LogError("Restart button is not assigned!");
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(MainMenu);  // Add listener for Main Menu button
        }
        else
        {
            Debug.LogError("Main Menu button is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            TogglePauseMenu();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void TogglePauseMenu()
    {
        if (gameOver)
        {
            return; // Prevent toggling pause when the game has ended
        }

        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(!isPaused);
        }
    }

    public void ShowGamePauseMenu()
    {
        if (!isPaused)
        {
            TogglePauseMenu();
        }
    }

    public void ShowGamePauseMenuDelay()
    {
        StartCoroutine(ShowGamePauseMenuCoroutine());
    }

    private IEnumerator ShowGamePauseMenuCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (!isPaused)
        {
            TogglePauseMenu();
        }
    }

    // Method to be called when the game ends (failure or success)
    public void EndGame()
{
    Debug.Log("EndGame() called: Game is over.");
    
    gameOver = true;  // Set the gameEnd flag
    pauseMenuUI.SetActive(true);  // Show the pause menu
    Time.timeScale = 0;  // Pause the game

    // Remove the Continue button
    if (continueButton != null)
    {
        continueButton.gameObject.SetActive(false);  // Hide the Continue button completely
        Debug.Log("Continue button is hidden.");
    }
    else
    {
        Debug.LogError("Continue button is not assigned!");
    }

    // Hide the Pause button
    if (pauseButton != null)
    {
        pauseButton.gameObject.SetActive(false);  // Hide the Pause button
        Debug.Log("Pause button is hidden.");
    }

    // Only Restart and Main Menu buttons remain visible and functional
}



    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}