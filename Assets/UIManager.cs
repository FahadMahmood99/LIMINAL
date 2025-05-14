using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject splashScreenPanel;
    public GameObject gameOverPanel;

    [Header("Game Over Panel")]
    public TMP_Text gameOverText;

    [Header("References")]
    public GameObject player;
    private Megan playerMovement;
    private Animator playerAnimator;
    private CharacterController characterController;

    private bool isPaused = false;

    void Start()
    {
        // Get player components
        if (player != null)
        {
            playerMovement = player.GetComponent<Megan>();
            playerAnimator = player.GetComponent<Animator>();
            characterController = player.GetComponent<CharacterController>();
        }

        // Only show splash screen in main menu scene
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Hide all panels first
            HideAllPanels();
            // Show main menu
            mainMenuPanel.SetActive(true);
            // Show splash screen
            ShowSplashScreen();
        }
        else
        {
            // In game scene, start with player movement enabled
            SetPlayerMovement(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Toggle pause menu with Escape key
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverPanel.activeSelf)
        {
            TogglePauseMenu();
        }
    }

    public void ShowSplashScreen()
    {
        // Hide all other panels
        HideAllPanels();
        
        // Show splash screen
        splashScreenPanel.SetActive(true);
        
        // Disable player movement
        SetPlayerMovement(false);
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // Hide all panels
        HideAllPanels();
        
        // Enable player movement
        SetPlayerMovement(true);
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TogglePauseMenu()
    {
        Debug.Log("TogglePauseMenu called. Current state: " + isPaused);
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Debug.Log("PauseMenuPanel active: " + pauseMenuPanel.activeSelf);
        
        // Toggle player movement
        SetPlayerMovement(!isPaused);
        
        // Toggle cursor
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        
        // Pause/unpause time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ShowGameOver(string message = "Game Over")
    {
        // Hide all other panels
        HideAllPanels();
        
        // Show game over panel
        gameOverPanel.SetActive(true);
        
        // Set game over message
        if (gameOverText != null)
        {
            gameOverText.text = message;
        }
        
        // Disable player movement
        SetPlayerMovement(false);
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Reset time scale
        Time.timeScale = 1f;
        
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        // Reset time scale
        Time.timeScale = 1f;
        
        // Load main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (splashScreenPanel != null) splashScreenPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void SetPlayerMovement(bool enabled)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = enabled;
            playerMovement.isGameOver = !enabled;
        }

        if (playerAnimator != null)
        {
            playerAnimator.enabled = enabled;
        }

        if (characterController != null)
        {
            characterController.enabled = enabled;
        }
    }
} 