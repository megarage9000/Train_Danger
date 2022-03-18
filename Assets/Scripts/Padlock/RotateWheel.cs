using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    bool isRotating;
    float rotation;
    public int number;
    public int wheelId;

    public static event Action<int, int> Rotated = delegate { };

    private void Awake()
    {
        rotation = 36f;
        isRotating = false;
    }

    public void OnRotate(bool isDownward)
    {
        if (!isRotating)
        {
            StartCoroutine(Rotate(isDownward));
        }
    }

    private IEnumerator Rotate(bool isDownward)
    {
        isRotating = true;
        float rotationInc = rotation / 10;
        rotationInc *= (!isDownward) ? -1 : 1;

        for(int i = 0; i < 10; i++)
        {
            transform.Rotate(0f, 0f, rotationInc);
            yield return new WaitForSeconds(0.01f);
        }

        number += (isDownward) ? -1 : 1;
        isRotating = false;
        Rotated(wheelId, number);
    }
}
