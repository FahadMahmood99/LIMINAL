using UnityEngine;



public class Door : MonoBehaviour
{
    public float openSpeed = 2f; // Speed of opening animation
    public Vector3 openPositionOffset = new Vector3(0, 3, 0); // How far it moves when opened
    
    public AudioClip doorSound;

    private Vector3 originalPosition;
    private bool isOpening = false;
    
    void Start()
    {
        originalPosition = transform.position;
    }
    
    public void OpenDoor()
    {
        isOpening = true;
                    // Play sound effect at camera position (better for 3D)
        if (doorSound != null)
        {
            AudioSource.PlayClipAtPoint(doorSound, Camera.main.transform.position);
        }
    }
    
    void Update()
    {
        if (isOpening)
        {
            // Smoothly move door to open position
            transform.position = Vector3.Lerp(
                transform.position, 
                originalPosition + openPositionOffset, 
                openSpeed * Time.deltaTime
            );
            
            // Optional: Destroy when fully open
            if (Vector3.Distance(transform.position, originalPosition + openPositionOffset) < 0.1f)
            {
                Destroy(gameObject); // Or disable collider instead
            }
        }
    }
}