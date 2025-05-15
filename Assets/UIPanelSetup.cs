using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanelSetup : MonoBehaviour
{
    [Header("Panel Prefabs")]
    public GameObject mainMenuPanelPrefab;
    public GameObject pauseMenuPanelPrefab;
    public GameObject splashScreenPanelPrefab;
    public GameObject gameOverPanelPrefab;

    [Header("References")]
    public Canvas mainCanvas;
    public UIManager uiManager;

    void Start()
    {
        if (mainCanvas == null)
        {
            Debug.LogError("Main Canvas reference is missing!");
            return;
        }

        if (uiManager == null)
        {
            Debug.LogError("UI Manager reference is missing!");
            return;
        }

        SetupPanels();
    }

    void SetupPanels()
    {
        // Create Main Menu Panel
        GameObject mainMenu = Instantiate(mainMenuPanelPrefab, mainCanvas.transform);
        mainMenu.name = "MainMenuPanel";
        uiManager.mainMenuPanel = mainMenu;

        // Create Pause Menu Panel
        GameObject pauseMenu = Instantiate(pauseMenuPanelPrefab, mainCanvas.transform);
        pauseMenu.name = "PauseMenuPanel";
        uiManager.pauseMenuPanel = pauseMenu;

        // Create Splash Screen Panel
        GameObject splashScreen = Instantiate(splashScreenPanelPrefab, mainCanvas.transform);
        splashScreen.name = "SplashScreenPanel";
        uiManager.splashScreenPanel = splashScreen;

        // Create Game Over Panel
        GameObject gameOver = Instantiate(gameOverPanelPrefab, mainCanvas.transform);
        gameOver.name = "GameOverPanel";
        uiManager.gameOverPanel = gameOver;

        // Setup button listeners
        SetupButtonListeners(mainMenu, "StartButton", uiManager.StartGame);
        SetupButtonListeners(mainMenu, "QuitButton", uiManager.QuitGame);
        SetupButtonListeners(pauseMenu, "ResumeButton", uiManager.TogglePauseMenu);
        SetupButtonListeners(pauseMenu, "MainMenuButton", uiManager.GoToMainMenu);
        SetupButtonListeners(pauseMenu, "QuitButton", uiManager.QuitGame);
        SetupButtonListeners(gameOver, "RestartButton", uiManager.RestartGame);
        SetupButtonListeners(gameOver, "MainMenuButton", uiManager.GoToMainMenu);

        // Get Game Over Text component
        Transform gameOverTextTransform = gameOver.transform.Find("GameOverText");
        if (gameOverTextTransform != null)
        {
            uiManager.gameOverText = gameOverTextTransform.GetComponent<TMP_Text>();
        }

        // Hide all panels initially
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        splashScreen.SetActive(false);
        gameOver.SetActive(false);
    }

    void SetupButtonListeners(GameObject panel, string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.transform.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
            else
            {
                Debug.LogWarning($"Button component not found on {buttonName}");
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found in panel {panel.name}");
        }
    }
} 