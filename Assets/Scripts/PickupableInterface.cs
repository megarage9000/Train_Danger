using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]
public class PickupableInterface : MonoBehaviour
{

    public UnityEvent OnDropObj;
    public UnityEvent OnPickupObj;

    Outline outline;
    protected Rigidbody rb;
    private void Awake()
    {
        outline = GetComponent<Outline>();
        rb = GetComponent<Rigidbody>();
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 10f;
        outline.enabled = false;
    }

    public void OnDetect()
    {
        outline.enabled = true;
    }

    public void OnLeave()
    {
        outline.enabled = false;
    }

    public void OnPickup(Transform parent)
    {
        if(rb.constraints != RigidbodyConstraints.None)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        rb.useGravity = false;
        rb.drag = 20;
        transform.parent = parent;
        OnPickupObj.Invoke();
    }

    public void UpdateHeldObject(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) > 0.1f)
        {
            Vector3 moveDir = (position - transform.position);
            rb.AddForce(moveDir * 50f);
        }
    }
    public void OnDrop()
    {
        rb.useGravity = true;
        rb.drag = 1;
        transform.parent = null;
        if (rb.freezeRotation)
        {
            rb.freezeRotation = false;
        }
        OnDropObj.Invoke();
    }

    public void OnFreezeToView()
    {
        Vector3 freezeRotation = new Vector3(0.0f, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(freezeRotation);
        rb.freezeRotation = true; 
    }

    public void UnfreezeView()
    {
        rb.freezeRotation = false;
        rb.AddForce(new Vector3(50, 0, 0));
    }
}
