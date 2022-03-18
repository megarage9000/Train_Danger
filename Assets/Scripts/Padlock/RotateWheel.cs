using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : InteractableInterface
{
    bool isRotating;
    float rotation;
    public int number;
    public int wheelId;

    public static event Action<int, int> Rotated = delegate { };

    private const int MAX_NUM = 10;
    private const int MIN_NUM = 1;

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
            // May need to change this in the future
            transform.Rotate(0f, rotationInc, 0f);
            yield return new WaitForSeconds(0.01f);
        }

        number += (isDownward) ? -1 : 1;
        number = (number > MAX_NUM) ? MIN_NUM : (number < MIN_NUM) ? MAX_NUM : number;
        isRotating = false;
        Rotated(wheelId, number);
    }

    public override void Interact()
    {
        if (!isRotating)
        {
            StartCoroutine(Rotate(true));
        }
    }
}
