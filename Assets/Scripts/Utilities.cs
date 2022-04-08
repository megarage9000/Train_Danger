using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{ 
    // Scans for specific objects in range, with a given mask
    public static GameObject GetObjectInRange(LayerMask layerMask, float range, Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            GameObject detectObject = hit.collider.transform.gameObject;
            return detectObject;
        }
        return null;
    }

    public static GameObject GetObjectInRange(float range, Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.white);
            GameObject detectObject = hit.collider.transform.gameObject;
            return detectObject;
        }
        return null;
    }

    public static void playSound(AudioSource audioSource, AudioClip clip)
    {
        if(audioSource)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(clip);
        }
    }
}
