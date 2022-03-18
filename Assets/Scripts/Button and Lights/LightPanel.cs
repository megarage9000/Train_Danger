using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanel : MonoBehaviour
{

    [SerializeField]
    public LightBulb[] lightBulbs;

    // Public properties we need for the button panel
    public int[] lightPattern = { 4, 3, 2, 1, 6, 5 };
    public Color[] lightColors;

    void Start()
    {
        int numBulbs = lightBulbs.Length;
        lightColors = new Color[lightBulbs.Length];
        for(int i = 0; i < numBulbs; i++)
        {
            lightColors[i] = lightBulbs[i].colour;
        }
        StartCoroutine(ShowPattern());   
    }

    IEnumerator ShowPattern()
    {
        int numBulbs = lightBulbs.Length;
        for(int i = 0; i < numBulbs; i++)
        {
            LightBulb bulb = lightBulbs[lightPattern[i] - 1];
            bulb.TurnOn();
            yield return new WaitForSeconds(1f);
            bulb.TurnOff();
        }
    }

}
