using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanelHint : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorObjs;

    [SerializeField]
    Color[] colors;

    private void Awake()
    {
        EnableColour();
    }

    public void NoColour()
    {
        int NumColors = colorObjs.Length;
        for (int i = 0; i < NumColors; i++)
        {
            colorObjs[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void EnableColour()
    {
        int NumColors = colorObjs.Length;
        for (int i = 0; i < NumColors; i++)
        {
            colorObjs[i].GetComponent<Renderer>().material.color = colors[i];
        }
    }
}
