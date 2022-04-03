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
    public Vector3 freezeView = new Vector3(0f, 0f, 0f);
    public Vector3 freezePositionOffsets = new Vector3(0f, 0f, 0f);

    [SerializeField]
    [Range(0, 1)]
    public float gripStrength = 0.5f;

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
        transform.position = parent.position;
        transform.rotation = parent.rotation;
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
        //freezePosition = transform.position;
        transform.localRotation = Quaternion.Euler(freezeView);
        Vector3 forwardOffset = transform.TransformDirection(freezePositionOffsets);
        transform.position += forwardOffset;
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezeView()
    {
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(new Vector3(50, 0, 0));
    }
}
