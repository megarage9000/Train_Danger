using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField]
    public Button[] buttons;
    public Color[] buttonColors;
    public int[] code;

    int enteredIndex;
    protected int numButtons;

    public static event Action OnError = delegate { };
    public static event Action OnSuccess = delegate { };

    protected void Awake()
    {
        numButtons = buttons.Length;
        for(int i = 0; i < numButtons; i++)
        {
            buttons[i].SetButtonColour(buttonColors[i]);
            buttons[i].buttonId = i;
        }
        Button.OnPressed += CheckResult;
        enteredIndex = 0;
    }

    protected void CheckResult(int index)
    {
        if ((index + 1) != code[enteredIndex])
        {
            print("Error! Invalid pattern");
            print("Pressed = " + index + 1 + ", expected" + code[enteredIndex]);
            enteredIndex = 0;
            OnError();
        }
        else
        {
            enteredIndex++;
            if(enteredIndex >= numButtons)
            {
                print("Success!");
                OnSuccess();
            }
        }
    }

}
