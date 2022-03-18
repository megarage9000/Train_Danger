using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanelLight : ButtonPanel
{
    [SerializeField]
    LightPanel lightPanel;
    private void Start()
    {
        lightPanel.lightPattern = code;
        for (int i = 0; i < numButtons; i++)
        {
            buttons[i].SetButtonColour(lightPanel.lightColors[i]);
            buttons[i].buttonPressColor = lightPanel.lightColors[i];
        }
        OnError.AddListener(lightPanel.StartShowingPattern);
    }
}
