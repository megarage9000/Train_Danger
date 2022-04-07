using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlacementHint))]
public class InitializePlacement : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placeableLocation;

    void Start()
    {
        PlaceableObject placeScript = objectToPlace.GetComponent<PlaceableObject>();
        if (placeScript)
        {
            placeScript.placeableLocation = placeableLocation;
            placeScript.OnPlace();
        }
    }

}
