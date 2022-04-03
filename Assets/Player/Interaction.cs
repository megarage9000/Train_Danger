using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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
    
    public Transform heldObjectPosition;
    public Rig HandRig;

    void Start()
    {
        heldObject = null;
        HandRig.weight = 0;
    }

    IEnumerator adjustHandRigWeight(float finalWeight)
    {
        while (HandRig.weight < finalWeight - 0.05)
        {
            HandRig.weight = Mathf.Lerp(HandRig.weight, finalWeight, 0.7f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator OpenHand()
    {
        while(HandRig.weight > 0.01)
        {
            HandRig.weight = Mathf.Lerp(HandRig.weight, 0, 0.7f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnPickup()
    {
        
        // Pick up object
        if(pickupable && pickupable.GetComponent<Rigidbody>() && heldObject == null)
        {

            heldObject = pickupable;
            PickupableInterface pickupScript = heldObject.GetComponent<PickupableInterface>();
            pickupScript.OnPickup(heldObjectPosition);
            pickupScript.OnFreezeToView();
            /*            StopAllCoroutines();
                        StartCoroutine(adjustHandRigWeight(1));*/
            HandRig.weight = pickupScript.gripStrength;
        }
        // Drop object
        else if(heldObject != null)
        {
            PickupableInterface pickupScript = heldObject.GetComponent<PickupableInterface>();
            pickupScript.UnfreezeView();
            pickupScript.OnDrop();
            heldObject = null;
            /*            StopAllCoroutines();
                        StartCoroutine(OpenHand());*/
            StartCoroutine(OpenHand());
        }
    }

    public void OnFreezeHeld()
    {
        if (heldObject)
        {
            // Check if object is placeable
            PlaceableObject placeableObject = heldObject.GetComponent<PlaceableObject>();
            if(placeableObject && placeableObject.OnPlace())
            {
                heldObject = null;
                pickupable = null;
                StartCoroutine(OpenHand());
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


    // Update is called once per frame
    void Update()
    {
        if(heldObject == null)
        {
            CheckPickupables();
        }
        CheckInteractables();
    }

    void CheckPickupables()
    {
        GameObject scannedPickupable = Utilities.GetObjectInRange(pickUpMask, interactionRange, transform);
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
        GameObject scannedInteractable = Utilities.GetObjectInRange(interactionMask, interactionRange, transform);
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
