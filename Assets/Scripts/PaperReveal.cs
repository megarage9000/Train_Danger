using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperReveal : MonoBehaviour
{

    public Material initialMaterial;
    Material codeMaterial;
    MeshRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        codeMaterial = renderer.material;
        renderer.material = initialMaterial;
    }

    public void RevealCode()
    {
        renderer.material = codeMaterial;
    }

}
