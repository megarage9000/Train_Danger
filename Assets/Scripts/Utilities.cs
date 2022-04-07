using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{ 
    // Scans for specific objects in range, with a given mask
    public static GameObject GetObjectInRange(LayerMask layerMask, float range, Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            GameObject detectObject = hit.collider.transform.gameObject;
            return detectObject;
        }
        return null;
    }

    public static GameObject GetObjectInRange(float range, Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            GameObject detectObject = hit.collider.transform.gameObject;
            return detectObject;
        }
        return null;
    }
}
