using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Refer to: https://www.youtube.com/watch?v=tXDgSGOEatk&t
public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    Vector2 horizontalInput;

    [SerializeField] float gravity = -9.81f; // or -9.81f
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float jumpHeight = 3.5f;
    bool isJump = false;

    [SerializeField] float scaleDown = 0.5f;
    [SerializeField] float crouchSpeedMultiplier = 0.5f;
    public Transform head;
    bool isCrouch = false;
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    // Update is called once per frame
    void Update()
    {

        // Checks if the transform position (feet) intersects with the ground mask
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if(isGrounded)
        {
            verticalVelocity.y = 0;
        }

        var vertSpeed = horizontalInput.y * speed * Time.deltaTime;
        var horzSpeed = horizontalInput.x * speed * Time.deltaTime;
        
        var movementSpeed = new Vector3(horzSpeed, 0, vertSpeed);
        movementSpeed = transform.TransformDirection(movementSpeed);
        controller.Move(movementSpeed);

        // Jump first before gravity
        if (isJump)
        {
            if (isGrounded)
            {
                // Jump equation
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            isJump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

    }

    public void OnJumpedPressed()
    {
        isJump = true;
    }

    public void OnCrouchPressed()
    {
        if (isGrounded)
        {
            isCrouch = !isCrouch;
            if(isCrouch)
            {
                Vector3 headPosition = head.position;
                headPosition.y *= scaleDown;
                head.position = headPosition;
                speed *= crouchSpeedMultiplier;
            }
            else
            {
                Vector3 headPosition = head.position;
                headPosition.y /= scaleDown;
                head.position = headPosition;
                speed /= crouchSpeedMultiplier;
            }
        }
    }
}
