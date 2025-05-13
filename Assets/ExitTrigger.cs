using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public string message = "You've Escaped!";
    public float delayBeforeReturn = 5f;
    public GameObject gameOverPanel; // Assign the GameOver Panel here
    public TMP_Text gameOverText; // Assign the Text here if you want to modify it
    public GameObject player; // Assign your player GameObject here
    private Megan playerMovement; // Reference to the animation_move.cs script
    private Animator playerAnimator; // Reference to Animator
    private CharacterController characterController; // Reference to CharacterController

    private bool hasExited = false;

    void Start()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<Megan>(); // Get the animation_move script
            playerAnimator = player.GetComponent<Animator>(); // Get the Animator
            characterController = player.GetComponent<CharacterController>(); // Get CharacterController
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasExited) return;

        if (other.CompareTag("Player"))
        {
            hasExited = true;

            // Show Game Over UI panel
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                if (gameOverText != null)
                {
                    gameOverText.text = message;
                }
            }

            // Disable player movement (assuming the player has the animation_move script)
            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Disable movement script
                playerMovement.isGameOver = true; // Stop player movement
            }

            // Disable Animator and CharacterController to stop animations and movement
            if (playerAnimator != null)
            {
                playerAnimator.enabled = false; // Stop animations
            }

            if (characterController != null)
            {
                characterController.enabled = false; // Disable character controller to stop movement
            }

            // Unlock cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Player escaped!");
        }
    }

    // For Restart Button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // For Main Menu Button
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your scene's name
    }
}
