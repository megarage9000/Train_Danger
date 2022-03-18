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
        if(!objectOutline)
        {
            print("Can't find outline");
        }
        else
        {
            print("Found outline!");
        }
        objectOutline.enabled = false;
        objectOutline.OutlineColor = Color.cyan;
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
