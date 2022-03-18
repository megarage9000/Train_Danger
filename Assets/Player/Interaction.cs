using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code mostly from: https://www.youtube.com/watch?v=GgLREaLUaac
public class Interaction : MonoBehaviour
{

    public float interactionRange = 4f;
    public float moveForce = 25f;
    public LayerMask pickUpMask;
    public LayerMask interactionMask;

    GameObject pickupable;
    GameObject interactable;

    GameObject heldObject;

    bool isHeldFrozen = false;
    
    public Transform heldObjectPosition;
    void Start()
    {
        heldObject = null;

        Vector3 pos = heldObjectPosition.position;
        heldObjectPosition.position = new Vector3(pos.x, pos.y, pos.z + interactionRange);
    }

    public void OnPickup()
    {
        
        // Pick up object
        if(pickupable && pickupable.GetComponent<Rigidbody>() && heldObject == null)
        {
            pickupable.GetComponent<PickupableInterface>().OnPickup(heldObjectPosition);
            heldObject = pickupable;
        }
        // Drop object
        else if(heldObject != null)
        {
            pickupable.GetComponent<PickupableInterface>().OnDrop();
            heldObject = null;
            pickupable = null;
        }
    }

    public void OnFreezeHeld()
    {
        if (heldObject)
        {
            PlaceableObject placeableObject = heldObject.GetComponent<PlaceableObject>();
            if(placeableObject && placeableObject.OnPlace())
            {
                heldObject = null;
                pickupable = null;
            }
            else
            {
                PickupableInterface pickupScript = heldObject.GetComponent<PickupableInterface>();
                if (!isHeldFrozen)
                {
                    pickupScript.OnFreezeToView();
                }
                else
                {
                    pickupScript.UnfreezeView();
                }
                isHeldFrozen = !isHeldFrozen;
            }
        }
    }

    public void OnInteract()
    {
        if (interactable)
        {
            interactable.GetComponent<InteractableInterface>().Interact();
        }
    }

    // Scans for specific objects in range, with a given mask
    GameObject GetObjectInRange(LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionRange, layerMask))
        {
            GameObject detectObject = hit.collider.transform.gameObject;
            return detectObject;
        }
        return null;
    }

    void MoveHeldObject()
    {
        if(heldObject == null)
        {
            return;
        }
        else
        {
            heldObject.GetComponent<PickupableInterface>().UpdateHeldObject(heldObjectPosition.position);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(heldObject == null)
        {
            CheckPickupables();
        }
        CheckInteractables();
        MoveHeldObject();
    }

    void CheckPickupables()
    {
        GameObject scannedPickupable = GetObjectInRange(pickUpMask);
        if (scannedPickupable)
        {
            if(scannedPickupable != pickupable && pickupable != null)
            {
                pickupable.GetComponent<PickupableInterface>().OnLeave();
            }
            scannedPickupable.GetComponent<PickupableInterface>().OnDetect();
            pickupable = scannedPickupable;
        }
        else if(pickupable != null)
        {
            pickupable.GetComponent<PickupableInterface>().OnLeave();
            pickupable = null;
        }
    }

    void CheckInteractables()
    {
        GameObject scannedInteractable = GetObjectInRange(interactionMask);
        if (scannedInteractable)
        {
            if(scannedInteractable != interactable && interactable != null)
            {
                interactable.GetComponent<InteractableInterface>().OnLeave();
            }
            scannedInteractable.GetComponent<InteractableInterface>().OnDetect();
            interactable = scannedInteractable;
        }
        else if(interactable != null)
        {
            interactable.GetComponent<InteractableInterface>().OnLeave();
            interactable = null;
        }
    }
}
