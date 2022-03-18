using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaceableObject : PickupableInterface
{
    GameObject placeableLocation;
    public string placeableLocationTag;

    public bool OnPlace()
    {
        if(placeableLocation)
        {
            PlacementHint hint = placeableLocation.GetComponent<PlacementHint>();
            if(hint)
            {
                OnDrop();
                Vector3 position = hint.GetPosition();
                Vector3 rotation = hint.DesiredRotation();
                transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.freezeRotation = true;
                hint.isObjectPlaced(true);
                OnLeave();
                return true;
            }
            return false;
        }
        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject detectedObject = collision.gameObject;
        if (detectedObject.CompareTag(placeableLocationTag))
        {
            placeableLocation = detectedObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject detectedObject = collision.gameObject;
        if (detectedObject.CompareTag(placeableLocationTag))
        {
            placeableLocation = null;
        }
    }
}
