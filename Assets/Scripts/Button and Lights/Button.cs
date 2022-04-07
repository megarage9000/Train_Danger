using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : InteractableInterface
{
    public int buttonId;
    public float pressDist;
    public Color buttonPressColor;
    public Color errorColor;
    public Color correctColor;
    public Vector3 direction;
    Renderer buttonRenderer;
    bool isPressed;

    public UnityEvent<int> OnPressed;
    public UnityEvent PlayNoise;

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

    public void SetErrorColor()
    {
        isPressed = true;
        SetButtonColour(errorColor);
    }

    public void SetCorrectColor()
    {
        isPressed = true;
        SetButtonColour(correctColor);
    }

    public void ResetColor()
    {
        isPressed = false;
        SetButtonColour(buttonPressColor);
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
        PlayNoise.Invoke();
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
        OnPressed.Invoke(buttonId);
        
    }
}
