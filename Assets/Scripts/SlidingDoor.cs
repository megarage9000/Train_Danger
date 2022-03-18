using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{

    public int distance;
    bool isMoving = false;
    private void Awake()
    {
        isMoving = false;
    }

    public void OpenDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(true));
        }
    }

    public void CloseDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(false));
        }
    }

    IEnumerator MoveDoor(bool isOpen)
    {
        isMoving = true;
        float increments = distance / 10f;
        increments *= (!isOpen) ? -1 : 1;
        Vector3 magnitude = transform.right * increments;

        for(int i = 0; i < 10; i++)
        {
            transform.Translate(magnitude);
            yield return new WaitForSeconds(0.02f);
        }
        isMoving = false;
    }
}
