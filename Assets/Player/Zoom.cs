using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    public Camera mainCam;
    public float targetZoom;
    float defaultZoom;

    public Coroutine zoomRoutine = null;

    bool zoom = false;

    private void Awake()
    {
        defaultZoom = mainCam.fieldOfView;
    }

    public void ToggleZoom()
    {
        zoom = !zoom;
        if (zoom)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        if(zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
        }

        zoomRoutine = StartCoroutine(adjustZoom(defaultZoom, targetZoom));
    }

    public void ZoomOut()
    {
        if (zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
        }

        zoomRoutine = StartCoroutine(adjustZoom(targetZoom, defaultZoom));
    }

    IEnumerator adjustZoom(float before, float after)
    {
        float fov = before;
        if(before < after)
        {
            while(before < after)
            {
                fov = Mathf.Lerp(fov, after, 0.5f);
                mainCam.fieldOfView = fov;
                yield return new WaitForSeconds(1.0f / 60.0f);
            }
        }
        else
        {
            while (before > after)
            {
                fov = Mathf.Lerp(fov, after, 0.5f);
                mainCam.fieldOfView = fov;
                yield return new WaitForSeconds(1.0f / 60.0f);
            }
        }
    }
}
