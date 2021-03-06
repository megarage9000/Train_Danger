using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Refer to: https://www.youtube.com/watch?v=tXDgSGOEatk&t

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Interaction playerInteraction;
    [SerializeField] Zoom playerZoom;

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
        interaction.Activate.performed += _ => playerInteraction.OnInteract();
        interaction.FreezeObject.performed += _ => playerInteraction.OnFreezeHeld();
        interaction.Zoom.performed += _ => playerZoom.ToggleZoom();

        // Crouch
        groundMovement.Crouch.performed += _ => movement.OnCrouchPressed();

        // Miscelleaneous (pause)
        misc.Pause.performed += _ =>
        {
            mouseLook.ToggleCursor();
        };
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
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
