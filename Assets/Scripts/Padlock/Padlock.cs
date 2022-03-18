using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Padlock : MonoBehaviour
{

    PadlockControls padlockControls;
    public Camera currCamera;
    public LayerMask layerMask;

    public int[] result;

    private void Awake()
    {
        padlockControls = new PadlockControls();
        var controls = padlockControls.Rotation;
        controls.RotateUp.performed += _ =>
        {
            CallWheelRotate(false);
        };
        controls.RotateDown.performed += _ =>
        {
            CallWheelRotate(true);
        };

        RotateWheel.Rotated += UpdateResult;
    }

    void CallWheelRotate(bool isDownward)
    {
        GameObject wheel = GetClickedObject();
        if(wheel)
        {
            RotateWheel rotateScript = wheel.GetComponent<RotateWheel>();
            if (rotateScript)
            {
                rotateScript.OnRotate(isDownward);
            }
        }
    }

    GameObject GetClickedObject()
    {
        RaycastHit hit;
        Vector3 coor = Mouse.current.position.ReadValue();
        if (Physics.Raycast(currCamera.ScreenPointToRay(coor), out hit, Mathf.Infinity, layerMask))
        {
            print(hit.collider.transform.name);
            return hit.collider.transform.gameObject;
        }
        return null;
    }

    public void OnEnable()
    {
        padlockControls.Enable();
    }

    public void OnDisable()
    {
        padlockControls.Disable();
    }

    private void UpdateResult(int wheelId, int value)
    {
        switch(wheelId)
        {
            case 1:
                result[0] = value;
                break;
            case 2:
                result[1] = value;
                break;
            case 3:
                result[2] = value;
                break;
            default:
                break;
        }

        print(result);
    }
}
