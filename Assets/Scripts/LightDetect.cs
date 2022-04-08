using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetect : MonoBehaviour
{
    const string otherTag = "LightDetect";
    public PaperReveal hint;


    private void OnTriggerEnter(Collider other)
    {
        GameObject detected = other.transform.gameObject;
        if (detected.CompareTag(otherTag))
        {
            print("detected light bulb!");
            hint.RevealCode();
        }
    }
}
