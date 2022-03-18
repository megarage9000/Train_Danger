using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public abstract class InteractableInterface : MonoBehaviour
{
    Outline objectOutline;

    void Start()
    {
        objectOutline = GetComponent<Outline>();
        objectOutline.enabled = false;
        objectOutline.OutlineColor = Color.cyan;
        objectOutline.OutlineWidth = 10f;
    }
    public void OnDetect()
    {
        objectOutline.enabled = true;
    }

    public void OnLeave()
    {
        objectOutline.enabled = false;
    }

    public abstract void Interact();
}
