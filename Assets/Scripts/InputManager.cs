using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;

    PlayerControls controls;
    PlayerControls.PlayerActions playerActions;

    Vector2 horizontalInput;
    Vector2 mouseInput;


    private void Awake()
    {
        controls = new PlayerControls();
        playerActions = controls.Player;

        // when horizontal movement happens assign the value to 'horizontalInput'
        playerActions.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        // jump pressed
        playerActions.Jump.performed += _ => movement.OnJumpPressed(); // use discard variable '_' since we don't need 'context'

        // head movement (look up/down, look left/right
        playerActions.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerActions.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        // sprint button 
        playerActions.Sprint.performed += _ => movement.OnSprintPressed();
        playerActions.Sprint.canceled += _ => movement.OnSprintCancelled();
    }


    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }


    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }
}
