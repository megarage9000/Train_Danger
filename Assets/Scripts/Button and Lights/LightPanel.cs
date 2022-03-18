using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanel : MonoBehaviour
{

    [SerializeField]
    public LightBulb[] lightBulbs;

    // Public properties we need for the button panel
    public int[] lightPattern;
    public Color[] lightColors;

    public bool isShowing;

    void Awake()
    {
        int numBulbs = lightBulbs.Length;
        lightColors = new Color[lightBulbs.Length];
        for(int i = 0; i < numBulbs; i++)
        {
            lightColors[i] = lightBulbs[i].colour;
        }
        isShowing = false;
    }

    public void StartShowingPattern()
    {
        if (!isShowing)
        {
            StartCoroutine(ShowPattern());
        }
    }

    IEnumerator ShowPattern()
    {
        isShowing = true;
        int numBulbs = lightBulbs.Length;
        for(int i = 0; i < numBulbs; i++)
        {
            LightBulb bulb = lightBulbs[lightPattern[i] - 1];
            bulb.TurnOn();
            yield return new WaitForSeconds(1f);
            bulb.TurnOff();
        }
        isShowing = false;
    }

}
