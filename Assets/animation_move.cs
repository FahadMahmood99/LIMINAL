using UnityEngine;

public class Megan : MonoBehaviour
{
    public bool isGameOver = false; // Track if the game is over

    public float gravity = -9.81f;
    private Vector3 velocity;
    public float moveSpeed = 2f;
    public float rotationSpeed = 700f;
    public Transform cameraTransform;

    private float mouseX, mouseY;
    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        // Get references to the components
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Lock and hide cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // If the game is over, stop processing movement
        if (isGameOver) return;
        // Handle Player Movement
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right Arrow for left-right movement
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down Arrow for forward/backward movement
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        if (characterController.isGrounded)
        {
            velocity.y = -2f; // Small value to ensure the character stays on the ground
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity when in the air
        }

        // Apply movement with CharacterController
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Set walking animation state
        bool isWalking = moveDirection.magnitude > 0.001f;  // Check if there's movement
        animator.SetBool("isWalking", isWalking);  // Update Animator parameter

        // Handle Camera Rotation (Mouse Look)
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Rotate the character (Megan) with mouse X-axis
        transform.Rotate(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);

        // Rotate the camera with mouse Y-axis (up/down movement)
        cameraTransform.Rotate(-mouseY * rotationSpeed * Time.deltaTime, 0f, 0f);

        // Clamp camera rotation to prevent flipping
        float cameraRotationX = cameraTransform.eulerAngles.x;
        if (cameraRotationX > 180f) cameraRotationX -= 360f;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -60f, 60f);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, cameraTransform.localRotation.eulerAngles.y, 0f);
    }
}
