using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float walkingSpeed = 3.0f;
    [SerializeField] private float sprintingSpeed = 10.0f;

    private Vector2 horizontalInput;
    private Vector3 verticalVelocity = Vector3.zero;
    private float gravity = -9.8f;
    private float jumpHeight = 1.0f;
    private bool isJumping = false;
    private bool isSprinting = false;


    private void Update()
    {
        // SPRINTING
        if (isSprinting == false)
        {
            // X and Y movement on the horizontal plane  
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * walkingSpeed;
            characterController.Move(horizontalVelocity * Time.deltaTime);
        }
        else if (isSprinting && isJumping == false)
        {
            // X and Y movement on the horizontal plane  
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * sprintingSpeed;
            characterController.Move(horizontalVelocity * Time.deltaTime);
        }
        

        // JUMPING
        if (isJumping)
        {
            isJumping = false;
            verticalVelocity.y = Mathf.Sqrt(-jumpHeight * gravity);
        }


        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }


    private bool IsGrounded()
    {
        // invisible sphere at the player position that checks when it touches the ground of a specify radius
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
    }


    public void ReceiveInput(Vector2 horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }


    public void OnJumpPressed()
    {
        if (IsGrounded())
            isJumping = true;
        
    }


    public void OnSprintPressed()
    {
        if (IsGrounded())
            isSprinting = true;
    }


    public void OnSprintCancelled()
    {
        isSprinting = false;
    }
}