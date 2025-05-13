using UnityEngine;
using UnityEngine.UI;  // Required for UI elements

public class SanityBarController : MonoBehaviour
{
    public Image sanityBar;  // Reference to the sanity bar Image
    public float maxSanity = 100f;  // Max value for the sanity
    public float currentSanity;  // Current value of sanity
    public float decrementRate = 1f;  // Rate at which sanity decreases

    void Start()
    {
        currentSanity = maxSanity;  // Start with full sanity
    }

    void Update()
    {
        DecreaseSanity();  // Decrease sanity over time
        UpdateSanityBar();  // Update the UI to reflect the new sanity
    }

    void DecreaseSanity()
    {
        // Decrease sanity over time
        if (currentSanity > 0)
        {
            currentSanity -= decrementRate * Time.deltaTime;  // Gradual decrease
        }
        else
        {
            currentSanity = 0;  // Ensure sanity doesn't go negative
        }
    }

    void UpdateSanityBar()
    {
        // Update the fill amount of the sanity bar
        if (sanityBar != null)
        {
            sanityBar.fillAmount = currentSanity / maxSanity;  // Set the fill amount based on the current sanity
        }
    }

    // You can add this function to increase sanity (for example, when the player performs actions that increase sanity)
    public void IncreaseSanity(float amount)
    {
        currentSanity += amount;
        if (currentSanity > maxSanity) currentSanity = maxSanity;  // Clamp to max sanity
    }
}
