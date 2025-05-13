using UnityEngine;
using TMPro;  // Don't forget to include this for TextMeshPro

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    [Tooltip("Assign your door GameObject here")]
    public GameObject targetDoorObject; // Changed from Door to GameObject

    private bool[] collectedKeys = new bool[5];
    private Door doorScript; // We'll cache the Door component
    
    // Add reference for the KeysCollectedText UI element
    public TMP_Text keysCollectedText; // Assign this in the Inspector

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            // Get the Door component reference
            if (targetDoorObject != null)
            {
                doorScript = targetDoorObject.GetComponent<Door>();
                
                if (doorScript == null)
                {
                    Debug.LogError("No Door script found on target door object!");
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CollectKey(int keyID)
    {
        if (keyID >= 0 && keyID < collectedKeys.Length)
        {
            collectedKeys[keyID] = true;
            UpdateKeysCollectedText();  // Update UI text whenever a key is collected
            CheckAllKeys();
        }
    }

    void CheckAllKeys()
    {
        foreach (bool key in collectedKeys)
        {
            if (!key) return;
        }
        
        // All keys collected - open the door
        if (doorScript != null)
        {
            doorScript.OpenDoor();
        }
        else
        {
            Debug.LogError("Cannot open door - no Door script reference!");
        }
    }

    // This method updates the keys collected text in the UI
    void UpdateKeysCollectedText()
    {
        int keysCollected = 0;
        foreach (bool key in collectedKeys)
        {
            if (key) keysCollected++;
        }
        
        // Update the UI with current key collection progress
        if (keysCollectedText != null)
        {
            keysCollectedText.text = "Keys Collected: " + keysCollected + "/5";
        }
    }
}
