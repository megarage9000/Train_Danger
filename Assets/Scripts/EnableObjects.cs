using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    [SerializeField]
    GameObject[] ObjectsToEnable;

    public void ShowObjects()
    {
        foreach(GameObject item in ObjectsToEnable)
        {
            item.SetActive(true);
        }
    }
}
