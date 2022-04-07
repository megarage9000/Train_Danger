using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(MeshRenderer))]
public class PlacementHint : MonoBehaviour
{
    public string otherTag;
    public Vector3 desiredRotation;
    MeshRenderer render;
    Outline outline;
    bool hasObject;

    public UnityEvent onObjectPlacedEvent;

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

    public void isObjectPlaced(bool value)
    {
        hasObject = value;
        render.enabled = !value;
        outline.enabled = !value;
        if(onObjectPlacedEvent != null)
        {
            onObjectPlacedEvent.Invoke();
        }
    }

    public bool hasObjectIn()
    {
        return hasObject;
    }
        
    public void ShowHint()
    {
        render.enabled = true;
        outline.enabled = true;
    }

    public void HideHint()
    {
        hasObject = false;
        render.enabled = false;
        outline.enabled = false;
    }
}
