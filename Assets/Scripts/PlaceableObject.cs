using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceableObject : PickupableInterface
{
    GameObject placeableLocation;
    public string placeableLocationTag;
    public UnityEvent OnPlaceObj;

    public bool OnPlace()
    {
        if(placeableLocation)
        {
            PlacementHint hint = placeableLocation.GetComponent<PlacementHint>();
            if(hint && hint.hasObjectIn() == false)
            {
                OnDrop();
                Vector3 position = hint.GetPosition();
                Vector3 rotation = hint.DesiredRotation();
                transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.freezeRotation = true;
                hint.isObjectPlaced(true);
                OnLeave();
                OnPlaceObj.Invoke();
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
            // Compare to the current one if it is closer;
            if (placeableLocation)
            {
                float dist1 = Vector3.Distance(placeableLocation.transform.position, transform.position);
                float dist2 = Vector3.Distance(detectedObject.transform.position, transform.position);

                placeableLocation = (dist1 > dist2) ? detectedObject : placeableLocation;
            }
            else
            {
                placeableLocation = detectedObject;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(placeableLocation == null)
        {
            GameObject detectedObject = collision.gameObject;
            if (detectedObject.CompareTag(placeableLocationTag))
            {
                placeableLocation = detectedObject;
            }
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
