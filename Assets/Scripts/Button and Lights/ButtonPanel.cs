using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : PuzzlerInterface
{
    [SerializeField]
    public Button[] buttons;
    public Color[] buttonColors;
    public Color errorColor;
    public Color correctColor;
    public int[] code;
    int codeLength;

    int enteredIndex;
    protected int numButtons;
    bool isFlashing;

    protected void Awake()
    {
        codeLength = code.Length;
        numButtons = buttons.Length;
        for(int i = 0; i < numButtons; i++)
        {
            buttons[i].SetButtonColour(buttonColors[i]);
            buttons[i].buttonPressColor = buttonColors[i];
            buttons[i].errorColor = errorColor;
            buttons[i].correctColor = correctColor;
            buttons[i].buttonId = i;
            buttons[i].OnPressed.AddListener(CheckResult);
        }
        isFlashing = false;
        OnError.AddListener(FlashError);
        OnSuccess.AddListener(FlashCorrect);
        enteredIndex = 0;
    }

    protected void CheckResult(int index)
    {
        if ((index + 1) != code[enteredIndex])
        {
            enteredIndex = 0;
            OnError.Invoke();
        }
        else
        {
            enteredIndex++;
            if(enteredIndex >= codeLength)
            {
                OnSuccess.Invoke();
            }
        }
    }

    private void FlashError()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashButtonColors(true));
        }
    }

    private void FlashCorrect()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashButtonColors(false));
        }
    }


    IEnumerator FlashButtonColors(bool isError)
    {
        isFlashing = true;
        for(int i = 0; i < numButtons; i++)
        {
            if (isError)
            {
                buttons[i].SetErrorColor();
            }
            else
            {
                buttons[i].SetCorrectColor();
            }
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < numButtons; i++)
        {
            buttons[i].ResetColor();
        }
        isFlashing = false;
    }

}
