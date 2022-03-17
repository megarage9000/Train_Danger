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
    bool isJump;
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
        print(horizontalInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
