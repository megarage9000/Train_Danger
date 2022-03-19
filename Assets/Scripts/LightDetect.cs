using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetect : MonoBehaviour
{
    const string otherTag = "LightDetect";
    public ColorPanelHint hint;

    private void Start()
    {
        hint.NoColour();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject detected = other.transform.gameObject;
        if (detected.CompareTag(otherTag))
        {
            print("detected light bulb!");
            hint.EnableColour();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject detected = other.transform.gameObject;
        if (detected.CompareTag(otherTag))
        {
            hint.NoColour();
        }
    }
}
