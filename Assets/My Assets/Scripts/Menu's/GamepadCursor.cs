using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;


public class GamepadCursor : MonoBehaviour
{
    private Mouse virtualMouse;
    private RectTransform cursorTransform;
    private float cursorSpeed = 1000.0f;
    private bool previousMouseState;

    private PlayerControls controls;
    private PlayerControls.PlayerActions playerActions;
    private PlayerControls.UIActions uIActions;

    //[SerializeField] private PlayerInput playerInput;


    private void Awake()
    {
        controls = new PlayerControls();
        playerActions = controls.Player;
        uIActions = controls.UI;


        playerActions.Disable();
        uIActions.Enable();


        uIActions.VirtualMouseValue.performed += _ => UpdateMotion();
    }


    private void OnEnable()
    {
        //print("In GamepadCursor.cs OnEnable() *********************************");

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added) // virtualMouse is not null and has not been added yet
        {
            InputSystem.AddDevice("VirtualMouse");
        }

        // Pair the device to the user to use the Player Input component with the Event System & the virtual mouse
        //InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 currentPosition = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, currentPosition);
        }

        //InputSystem.onAfterUpdate += UpdateMotion;

    }


    private void OnDisable()
    {
        //InputSystem.onAfterUpdate -= UpdateMotion;
    }


    private void UpdateMotion()
    {
        print("In UpdateMotion() &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");

        // If there is no virtual mouse or gamepad then exit this method
        if (virtualMouse == null || Gamepad.current == null)
            return;

        // Stick delta
        Vector2 stickDelta = Gamepad.current.leftStick.ReadValue();
        stickDelta *= cursorSpeed * Time.deltaTime;

        print(stickDelta);

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + stickDelta;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, stickDelta);

        //bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        //if (previousMouseState != aButtonIsPressed)
        //{
        //    virtualMouse.CopyState<MouseState>(out var mouseState);
        //    mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
        //    InputState.Change(virtualMouse, mouseState);
        //    previousMouseState = aButtonIsPressed;
        //}

    }
}
