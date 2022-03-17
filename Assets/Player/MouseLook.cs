using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Refer to: https://www.youtube.com/watch?v=tXDgSGOEatk&t
public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 4f;
    [SerializeField] float sensitivityY = 4f;

    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    bool toggleCursor = true;

    public void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleCursor)
        {
            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

            // Rotating camera
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            playerCamera.eulerAngles = targetRotation;
        }
    }

    public void ToggleCursor()
    {
        toggleCursor = !toggleCursor;
        if(toggleCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
