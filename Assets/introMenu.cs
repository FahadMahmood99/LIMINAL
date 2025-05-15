using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroMenu : MonoBehaviour
{
    public GameObject guidePanel;
    public GameObject menuCanvas;
    public GameObject player;

    public GameObject gameOverCanvas;
    public Button guideButton;        // Assigned in Inspector
    public Button closeGuideButton;   // Assigned in Inspector
    public MonoBehaviour[] scriptsToDisable;

    private bool menuActive = true;

    void Start()
    {
        // Hide guide panel initially
        guidePanel.SetActive(false);

        // Hide game over canvas initially
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); // ✅ NEW LINE

        // Assign button listeners
        guideButton.onClick.AddListener(ToggleGuide);
        closeGuideButton.onClick.AddListener(CloseGuide);

        // Disable player control scripts
        foreach (var script in scriptsToDisable)
            script.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        if (!menuActive) return;

        // Keyboard input
        if (Input.anyKeyDown)
        {
            // Press G key: toggle guide panel
            if (Input.GetKeyDown(KeyCode.G))
            {
                ToggleGuide();
            }
            // Any other key: start game ONLY if guide is not active
            else if (!guidePanel.activeSelf)
            {
                StartGame();
            }
        }

        // Escape key closes guide if open
        if (guidePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGuide();
        }
    }

    public void StartGame()
    {
        if (!menuActive) return;

        menuCanvas.SetActive(false);
        guidePanel.SetActive(false);
        menuActive = false;

        // Enable gameplay scripts
        foreach (var script in scriptsToDisable)
            script.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleGuide()
    {
        guidePanel.SetActive(!guidePanel.activeSelf);
    }

    public void CloseGuide()
    {
        // Just hide the guide, stay in menu
        guidePanel.SetActive(false);
    }
}
