using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkDoor : MonoBehaviour
{
    public float rotation;
    bool isMoving;
    private void Awake()
    {
        isMoving = false;
    }
    public void Open()
    {
        if(!isMoving)
        {
            StartCoroutine(MoveDoor(true));
        }
    }

    IEnumerator MoveDoor(bool direction)
    {
        isMoving = true;
        float increment = rotation / 10f;
        increment *= (direction) ? 1 : -1; 
        for(int i = 0; i < 10; i++)
        {
            float prevRotationX = transform.rotation.x;
            transform.Rotate(Vector3.right, prevRotationX + increment);
            yield return new WaitForSeconds(0.05f);
        }
        isMoving = false;
    }
}
