using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameEventSO OnLoadPauseScene;
    [SerializeField] private Movement movement;
    [SerializeField] private MouseLook mouseLook;

    private PlayerControls controls;
    private PlayerControls.PlayerActions playerActions;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;
    private bool isGamePaused;


    private void Awake()
    {
        // Hide mouse cursor at the centre of screen when in game
        Cursor.lockState = CursorLockMode.Locked;

        // Set time scale to normal speed
        Time.timeScale = 1;

        controls = new PlayerControls();
        playerActions = controls.Player;

        #region PLAYER

        // When horizontal movement happens assign the value to 'horizontalInput'
        playerActions.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        // Jump pressed
        playerActions.Jump.performed += _ => movement.OnJumpPressed(); // use discard variable '_' since we don't need 'context'

        // Head movement (look up/down, look left/right
        playerActions.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerActions.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        // Sprint button 
        playerActions.Sprint.performed += _ => movement.OnSprintPressed();
        playerActions.Sprint.canceled += _ => movement.OnSprintCancelled();

        // Pause pressed
        playerActions.Pause.performed += _ => OnPausePressed();

        #endregion PLAYER

    }


    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }


    private void OnPausePressed()
    {
        if (isGamePaused == false)
        {
            isGamePaused = true;

            // Show mouse cursor when in menus
            Cursor.lockState = CursorLockMode.None;

            // Set time scale to zero speed which stops any player movement
            Time.timeScale = 0;

            // Raise event to the LevelManager method 'LoadPauseMenu'
            OnLoadPauseScene.Raise();

            // Stop controls of player
            controls.Player.Disable();
        }
    }


    public void OnUnloadPauseScene()
    {
        isGamePaused = false;

        // Hide mouse cursor at the centre of screen when go back to game
        Cursor.lockState = CursorLockMode.Locked;

        // Set time scale back to normal speed
        Time.timeScale = 1;

        // Stop controls of player
        controls.Player.Enable();
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
