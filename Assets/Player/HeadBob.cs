using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Massive thanks to
// https://www.youtube.com/watch?v=5MbR2qJK8Tc
public class HeadBob : MonoBehaviour
{
    bool enable = false;

    [SerializeField, Range(0, 1.0f)] private float amp = 0.015f;
    [SerializeField, Range(0, 30)] private float freq = 10f;

    public Transform cameraPos;
    public Transform cameraHolder;

    float toggleSpeed = 3.0f;
    Vector3 startPos;

    private void Awake()
    {
        startPos = cameraPos.localPosition;
    }

    public void PlayFootstepMotion()
    {
        cameraPos.localPosition += FootstepMotion();
    }

    private void ResetPosition()
    {
        if (cameraPos.localPosition == startPos) return;
        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FootstepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * freq) * amp;
        pos.x += Mathf.Cos(Time.time * freq / 2) * amp * 2;
        return pos;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        ResetPosition();
        cameraPos.LookAt(FocusTarget());
    }
}
