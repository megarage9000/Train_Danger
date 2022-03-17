using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Refer to: https://www.youtube.com/watch?v=tXDgSGOEatk&t

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Interaction playerInteraction;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.InteractionActions interaction;
    PlayerControls.MiscActions misc;
    
    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        interaction = controls.Interaction;
        misc = controls.Misc;


        // groundMovement.[action].performed += context => do something
        // Essentially reads a vector 2 input coming from the input system
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        // Directly call jump instead of updating
        groundMovement.Jump.performed += _ => movement.OnJumpedPressed();

        // Mouse movement
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        // Interaction components
        interaction.Pickup.performed += _ => playerInteraction.OnPickup();

        // Crouch
        groundMovement.Crouch.performed += _ => movement.OnCrouchPressed();

        // Miscelleaneous (pause)
        misc.Pause.performed += _ =>
        {
            mouseLook.ToggleCursor();
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }
}
