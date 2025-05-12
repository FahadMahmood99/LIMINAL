using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public string message = "You've Escaped!";
    public float delayBeforeReturn = 5f;
    public GameObject messageUI; // Assign TMP object here

    private bool hasExited = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasExited) return;

        if (other.CompareTag("Player"))
        {
            hasExited = true;

            if (messageUI != null)
            {
                messageUI.SetActive(true);
                TMP_Text text = messageUI.GetComponent<TMP_Text>();
                if (text != null)
                {
                    text.text = message;
                }
            }

            Debug.Log("Player escaped!");
            Invoke("ReturnToMainMenu", delayBeforeReturn);
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Update to your actual main menu scene name
    }
}
