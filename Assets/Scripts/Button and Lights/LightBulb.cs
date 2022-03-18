using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightBulb : MonoBehaviour
{
    public Color colour;
    [SerializeField]
    public Material[] materials;

    Renderer lightRenderer;

    private void Awake()
    {
        lightRenderer = GetComponent<Renderer>();
        TurnOff();
    }

    public void TurnOn()
    {
        lightRenderer.material = materials[1];
        lightRenderer.material.SetColor("_Color", colour);
        lightRenderer.material.EnableKeyword("_EMISSION");
        lightRenderer.material.SetColor("_EmissionColor", colour);
    }

    public void TurnOff()
    {
        lightRenderer.material = materials[0];
        lightRenderer.material.DisableKeyword("_EMISSION");
    }
}
