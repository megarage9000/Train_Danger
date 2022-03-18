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

    
    public Transform heldObjectPosition;
    // Start is called before the first frame update
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
            Rigidbody rb = pickupable.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.drag = 20;
            rb.transform.parent = heldObjectPosition;
            heldObject = pickupable;
        }
        // Drop object
        else if(heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.drag = 1;
            rb.transform.parent = null;
            heldObject = null;
            pickupable = null;
        }
    }

    public void OnInteract()
    {
        interactable.GetComponent<InteractableInterface>().Interact();
    }

    // Scans for specific objects in range, with a given mask
    GameObject GetObjectInRange(LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionRange, layerMask))
        {
            GameObject detectObject = hit.collider.transform.gameObject;
            InteractableInterface interactable = detectObject.GetComponent<InteractableInterface>();
            if (interactable)
            {
                interactable.OnDetect();
            }
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
        if(Vector3.Distance(heldObject.transform.position, heldObjectPosition.position) > 0.1f)
        {
            Vector3 moveDir = (heldObjectPosition.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDir * moveForce);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(heldObject == null)
        {
            pickupable = GetObjectInRange(pickUpMask);
        }
        CheckInteractables();
        MoveHeldObject();
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
        }
    }
}
