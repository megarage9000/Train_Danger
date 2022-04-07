using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Refer to: https://www.youtube.com/watch?v=tXDgSGOEatk&t
public class Movement : MonoBehaviour
{
    // [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    Vector2 horizontalInput;

    [SerializeField] float gravity = -9.81f; // or -9.81f
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float jumpHeight = 3.5f;
    bool isJump = false;

    [SerializeField] float scaleDown = 0.5f;
    [SerializeField] float crouchSpeedMultiplier = 0.5f;
    public Transform head;
    bool isCrouch = false;

    Rigidbody rb;
    AudioSource footstepSource;

    [SerializeField]
    public UnityEvent OnPlayerMove;

    Coroutine crouchRoutine = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        footstepSource = GetComponent<AudioSource>();
    }
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        // Jump first before gravity
        if (isJump)
        {
            if (isGrounded && !isCrouch)
            {
                if (crouchRoutine != null)
                {
                    StopCoroutine(crouchRoutine);
                }
                // Jump equation
                var calcJump = Mathf.Sqrt(-2f * jumpHeight * gravity);
                rb.AddForce(Vector3.up * calcJump, ForceMode.VelocityChange);
            }
            isJump = false;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var vertSpeed = horizontalInput.y * speed * Time.fixedDeltaTime;
        var horzSpeed = horizontalInput.x * speed * Time.fixedDeltaTime;

        var movementSpeed = new Vector3(horzSpeed, 0, vertSpeed);
        movementSpeed = transform.TransformDirection(movementSpeed);

        if(movementSpeed.magnitude > 0f)
        {
            OnPlayerMove.Invoke();
            PlayFootsteps();
        }

        rb.MovePosition(transform.position + movementSpeed);
    }

    public void OnJumpedPressed()
    {
        isJump = true;
    }

    IEnumerator AdjustHeight(float multiplier)
    {
        Vector3 headPosition = head.position;
        float desiredCrouch = headPosition.y * multiplier;
        float height = headPosition.y;

        if(height < desiredCrouch)
        {
            while(desiredCrouch >= height)
            {
                height = Mathf.Lerp(height, desiredCrouch, 0.5f);
                headPosition = head.position;
                head.position = new Vector3(headPosition.x, height, headPosition.z);
                yield return new WaitForSeconds(1.0f / 120f);
            }
        }
        else
        {
            while (desiredCrouch <= height)
            {
                height = Mathf.Lerp(height, desiredCrouch, 0.5f);
                headPosition = head.position;
                head.position = new Vector3(headPosition.x, height, headPosition.z);
                yield return new WaitForSeconds(1.0f / 120f);
            }
        }
    }

    
    public void OnCrouchPressed()
    {
        if (isGrounded)
        {
            isCrouch = !isCrouch;
            if(isCrouch)
            {
                if(crouchRoutine != null)
                {
                    StopCoroutine(crouchRoutine);
                }
                crouchRoutine = StartCoroutine(AdjustHeight(scaleDown));
                speed *= crouchSpeedMultiplier;
            }
            else
            {
                if (crouchRoutine != null)
                {
                    StopCoroutine(crouchRoutine);
                }
                crouchRoutine = StartCoroutine(AdjustHeight(1.0f / scaleDown));
                speed /= crouchSpeedMultiplier;
            }
        }
    }

    bool isFootstepPlaying = false;
    IEnumerator PlayFootstepSound()
    {
        if (!isFootstepPlaying)
        {
            isFootstepPlaying = true;
            footstepSource.volume = Random.Range(0.8f, 1.0f);
            footstepSource.pitch = Random.Range(0.8f, 1.0f);
            footstepSource.Play();
            yield return new WaitForSeconds(0.5f);
            isFootstepPlaying = false;
        }
    }

    void PlayFootsteps()
    {
        if(isGrounded && !footstepSource.isPlaying)
        {
            StartCoroutine(PlayFootstepSound());
        }
    }
}
