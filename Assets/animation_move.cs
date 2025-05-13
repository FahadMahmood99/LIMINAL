//using UnityEngine;

//public class Megan : MonoBehaviour
//{
//    public bool isGameOver = false; // Track if the game is over

//    public float gravity = -9.81f;
//    private Vector3 velocity;
//    public float moveSpeed = 2f;
//    public float rotationSpeed = 700f;
//    public Transform cameraTransform;

//    private float mouseX, mouseY;
//    private CharacterController characterController;
//    private Animator animator;

//    void Start()
//    {
//        // Get references to the components
//        characterController = GetComponent<CharacterController>();
//        animator = GetComponent<Animator>();

//        // Lock and hide cursor for mouse look
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }

//    void Update()
//    {
//        // If the game is over, stop processing movement
//        if (isGameOver) return;
//        // Handle Player Movement
//        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right Arrow for left-right movement
//        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down Arrow for forward/backward movement
//        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

//        if (characterController.isGrounded)
//        {
//            velocity.y = -2f; // Small value to ensure the character stays on the ground
//        }
//        else
//        {
//            velocity.y += gravity * Time.deltaTime; // Apply gravity when in the air
//        }

//        // Apply movement with CharacterController
//        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

//        // Set walking animation state
//        bool isWalking = moveDirection.magnitude > 0.001f;  // Check if there's movement
//        animator.SetBool("isWalking", isWalking);  // Update Animator parameter

//        // Handle Camera Rotation (Mouse Look)
//        mouseX = Input.GetAxis("Mouse X");
//        mouseY = Input.GetAxis("Mouse Y");

//        // Rotate the character (Megan) with mouse X-axis
//        transform.Rotate(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);

//        // Rotate the camera with mouse Y-axis (up/down movement)
//        cameraTransform.Rotate(-mouseY * rotationSpeed * Time.deltaTime, 0f, 0f);

//        // Clamp camera rotation to prevent flipping
//        float cameraRotationX = cameraTransform.eulerAngles.x;
//        if (cameraRotationX > 180f) cameraRotationX -= 360f;
//        cameraRotationX = Mathf.Clamp(cameraRotationX, -60f, 60f);
//        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, cameraTransform.localRotation.eulerAngles.y, 0f);
//    }
//}
//------------------------------------------------------------------------------------------------------------------------------
using UnityEngine;

public class Megan : MonoBehaviour
{
    public bool isGameOver = false;

    public float gravity = -9.81f;
    private Vector3 velocity;
    public float moveSpeed = 2f;
    public float rotationSpeed = 700f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
 
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private Transform groundCheck;

    void Update()
    {
        if (isGameOver) return;

        // Horizontal look (rotate player)
        float mouseX = lookInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Vertical look (clamp before applying)
        float mouseY = lookInput.y * rotationSpeed * Time.deltaTime;
        Vector3 currentRotation = cameraTransform.localEulerAngles;
        float desiredX = currentRotation.x - mouseY;

        if (desiredX > 180f) desiredX -= 360f;
        desiredX = Mathf.Clamp(desiredX, -60f, 60f);

        cameraTransform.localEulerAngles = new Vector3(desiredX, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        // Move
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Keep movement on horizontal plane
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camRight * moveInput.x + camForward * moveInput.y;

        // 90-degree turning logic


        // Apply movement
        Vector3 horizontalVelocity = direction * moveSpeed;

        // Gravity handling
        bool grounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayer);
        if (grounded)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.fixedDeltaTime;

        // Final move
        Vector3 finalVelocity = horizontalVelocity;
        finalVelocity.y = velocity.y;

        rb.velocity = finalVelocity;

        // Animation
        bool isWalking = direction.magnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);
    }


}

