using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Padlock : MonoBehaviour
{
    public int[] result;
    private void Awake()
    {
        RotateWheel.Rotated += UpdateResult;
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

        print(result[0] + ", " + result[1] + ", " + result[2]);
    }
}
