using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableInterface
{


    public int buttonId;
    public float pressDist;
    public Color buttonPressColor;
    public Vector3 direction;
    Renderer buttonRenderer;
    Color initialColor;
    bool isPressed;

    public static event Action<int> OnPressed = delegate { };

    public void Awake()
    {
        buttonRenderer = GetComponent<Renderer>();
        buttonRenderer.material.color = buttonPressColor;
        isPressed = false;
    }

    public void SetButtonColour(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public override void Interact()
    {
        if(!isPressed)
        {
            StartCoroutine(OnPress());
        }
    }

    IEnumerator OnPress()
    {
        isPressed = true; 
        float increment = pressDist / 10f;
        Vector3 translation = direction * pressDist;
        for(int i = 0; i < 10; i++)
        {
            transform.Translate(translation);
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < 10; i++)
        {
            transform.Translate(-translation);
            yield return new WaitForSeconds(0.02f);
        }
        isPressed = false;
        OnPressed(buttonId);
    }
}
