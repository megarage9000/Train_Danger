using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(MeshRenderer))]
public class PlacementHint : MonoBehaviour
{
    public string otherTag;
    public Vector3 desiredRotation;
    MeshRenderer render;
    Outline outline;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();

        render.enabled = false;
        outline.enabled = false;
        outline.OutlineColor = Color.black;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 DesiredRotation()
    {
        return transform.rotation.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject entered = other.gameObject;
        if (entered.CompareTag(otherTag))
        {
            render.enabled = true;
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject entered = other.gameObject;
        if (entered.CompareTag(otherTag))
        {
            render.enabled = false;
            outline.enabled = false;
        }
    }
}
