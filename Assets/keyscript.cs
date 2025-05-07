using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyID = 0; // Set this to 0-4 for each key
    public GameObject pickupEffect;
    public AudioClip pickupSound; // Assign your MP3 in Inspector
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play sound effect at camera position (better for 3D)
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
            }
            
            // Null check for safety
            if (KeyManager.instance != null)
            {
                KeyManager.instance.CollectKey(keyID);
            }
            else
            {
                Debug.LogError("KeyManager instance not found!");
            }
            
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}