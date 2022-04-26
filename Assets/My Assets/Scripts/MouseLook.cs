using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 80.0f;

    [SerializeField] float sensitivityX = 20.0f;
    [SerializeField] float sensitivityY = 0.2f;

    private float mouseX, mouseY;
    private float xRotation;


    private void Update()
    {
        // rotate the transform around the Y vector by the mouse X delta 
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        // to invert up and down change to '+='
        xRotation -= mouseY;
        // clamp the rotation to a specific amount so you don't loop around unrealistically
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}
