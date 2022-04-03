using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceableObject : PickupableInterface
{
    GameObject placeableLocation;
    public string placeableLocationTag;
    public UnityEvent OnPlaceObj;
    public float range = 4f;

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

    public void Update()
    {
        GameObject gameObject = Utilities.GetObjectInRange(range, transform);
        if (gameObject && gameObject.CompareTag(placeableLocationTag))
        {
            if (placeableLocation)
            {
                HidePreviousHint();
            }

            placeableLocation = gameObject;
            ShowNewHint();

        }
        else if(placeableLocation != null)
        {
            HidePreviousHint();
        }
    }

    void ShowNewHint()
    {
        PlacementHint hintScript = placeableLocation.GetComponent<PlacementHint>();
        if (hintScript)
        {
            hintScript.ShowHint();
        }
    }

    void HidePreviousHint()
    {
        PlacementHint hintScript = placeableLocation.GetComponent<PlacementHint>();
        if (hintScript)
        {
            hintScript.HideHint();
        }
    }
}
