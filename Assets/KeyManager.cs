using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;
    
    [Tooltip("Assign your door GameObject here")]
    public GameObject targetDoorObject; // Changed from Door to GameObject
    
    private bool[] collectedKeys = new bool[5];
    private Door doorScript; // We'll cache the Door component
    
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
}