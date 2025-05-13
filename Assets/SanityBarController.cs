using UnityEngine;
using UnityEngine.UI;  // Required for UI elements

public class SanityBarController : MonoBehaviour
{
    public Image sanityBar;  // Reference to the sanity bar Image
    public float maxSanity = 100f;  // Max value for the sanity
    public float currentSanity;  // Current value of sanity

    public SanityEffects sanity;
    void Start()
    {
        currentSanity = sanity.sanity;  // Start with full sanity
    }

    void Update()
    {
        currentSanity = sanity.sanity;
        UpdateSanityBar();  // Update the UI to reflect the new sanity
    }



    void UpdateSanityBar()
    {
        // Update the fill amount of the sanity bar
        if (sanityBar != null)
        {
            sanityBar.fillAmount = currentSanity / maxSanity;  // Set the fill amount based on the current sanity
        }
    }


}
