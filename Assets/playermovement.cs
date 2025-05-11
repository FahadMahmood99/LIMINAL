//using UnityEngine;

//public class SpherePlayerController : MonoBehaviour
//{
//    // Movement settings
//    public float moveSpeed = 2f;
//    // public float rotationSpeed = 5f;
    
//    // Camera settings
//    public float cameraDistance = 5f;
//    public float cameraHeight = 2f;
//    public float mouseSensitivity = 2f;
    
//    private float cameraRotationX = 0f;
//    private float cameraRotationY = 0f;
//    private Rigidbody rb;
//    private GameObject cameraPivot;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
        
//        // Create camera pivot if it doesn't exist
//        if (cameraPivot == null)
//        {
//            cameraPivot = new GameObject("Camera Pivot");
//            cameraPivot.transform.position = transform.position + Vector3.up * cameraHeight;
//        }
        
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }

//    void Update()
//    {
//        // Update camera pivot position to follow sphere
//        cameraPivot.transform.position = transform.position + Vector3.up * cameraHeight;
        
//        // Handle mouse look
//        cameraRotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
//        cameraRotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
//        cameraRotationY = Mathf.Clamp(cameraRotationY, -30f, 70f);
        
//        // Rotate camera pivot
//        cameraPivot.transform.rotation = Quaternion.Euler(cameraRotationY, cameraRotationX, 0);
        
//        // Position camera behind the pivot
//        Camera.main.transform.position = cameraPivot.transform.position - cameraPivot.transform.forward * cameraDistance;
//        Camera.main.transform.LookAt(cameraPivot.transform.position);
//    }

//    void FixedUpdate()
//    {
//        // Handle movement
//        float horizontal = Input.GetAxis("Horizontal");
//        float vertical = Input.GetAxis("Vertical");
        
//        // Get camera forward direction without vertical component
//        Vector3 cameraForward = cameraPivot.transform.forward;
//        cameraForward.y = 0;
//        cameraForward.Normalize();
        
//        Vector3 movement = (cameraForward * vertical + cameraPivot.transform.right * horizontal).normalized * moveSpeed;
        
//        // Apply movement
//        rb.AddForce(movement);
        
//        // Optional: Rotate sphere slightly in movement direction
//        // if (movement.magnitude > 0.1f)
//        // {
//        //     Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
//        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
//        // }
//    }
//}