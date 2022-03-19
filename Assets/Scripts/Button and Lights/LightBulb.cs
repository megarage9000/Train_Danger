using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightBulb : MonoBehaviour
{
    public Color colour;
    [SerializeField]
    public Material[] materials;
    [SerializeField]
    Collider lightDetector;

    Renderer lightRenderer;
    Light light;

    private void Awake()
    {
        colour *= 5f;
        lightRenderer = GetComponent<Renderer>();
        if (!lightRenderer)
        {
            lightRenderer = GetComponentInChildren<Renderer>();
        }
        light = GetComponentInChildren<Light>();
        if (light)
        {
            light.color = colour;
        }
        TurnOff();
    }

    public void TurnOn()
    {
        if (light)
        {
            light.enabled = true;
        }
        lightRenderer.material = materials[1];
        lightRenderer.material.SetColor("_Color", colour);
        lightRenderer.material.EnableKeyword("_EMISSION");
        lightRenderer.material.SetColor("_EmissionColor", colour);
        lightDetector.enabled = true;
    }

    public void TurnOff()
    {
        if (light)
        {
            light.enabled = false;
        }
        lightRenderer.material = materials[0];
        lightRenderer.material.DisableKeyword("_EMISSION");
        lightDetector.enabled = false;
    }
}
