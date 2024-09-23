using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public static MovementScript Instance;
    public float amplitude;
    public float frequency;
    public float speed;
    public float accelerationSpeed;
    public float currentSpeed;
    public float rotationSpeed;
    public float hor, vert, upAndDown;
    public float normalSpeed;
    public float slowerSpeed;
    public Animator animator;
    private Transform cam;
    private Vector3 dir;
    private Rigidbody rb;

    //Ground Movement
    public bool isWalking;
    public bool isTogglingMovement;
    public Animator moveChestAnimator;
    public float startFlyingSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputCheck();
        TogglingMovement();
        FlyMovement();
        WalkingMovement();
        RotationFlyChange();
        RotationWalkChange();
    }

    private void InputCheck()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        upAndDown = Input.GetAxisRaw("UpAndDown");
        isTogglingMovement = Input.GetKeyDown(KeyCode.T);
    }

    private void FlyMovement()
    {
        if (!isWalking)
        {
            currentSpeed = rb.velocity.magnitude;
            dir = cam.transform.forward * vert + cam.transform.right * hor + Vector3.up * upAndDown;
            dir.Normalize();
            rb.AddForce(dir * accelerationSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
                     
    }

    private void RotationFlyChange()
    {
        if (dir.magnitude != 0 && !isWalking)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, cam.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void MakeSpeedSlower()
    {
        accelerationSpeed = slowerSpeed;
    }

    public void MakeSpeedNormal()
    {
        accelerationSpeed = normalSpeed;
    }

    private void TogglingMovement()
    {
        if (isTogglingMovement && !isWalking) 
        {
            isWalking = true;
            animator.SetBool("IsWalking", true);
            rb.useGravity = true;
            moveChestAnimator.SetTrigger("PutBack");
        }

        else if (isTogglingMovement && isWalking)
        {
            isWalking = false;
            animator.SetBool("IsWalking", false);
            rb.useGravity = false;
            moveChestAnimator.SetTrigger("PutFront");
            rb.AddForce(Vector3.up * startFlyingSpeed, ForceMode.VelocityChange);
        }
    }

    private void WalkingMovement()
    {
        if (isWalking)
        {
            currentSpeed = rb.velocity.magnitude;
            dir = cam.transform.forward * vert + cam.transform.right * hor;
            dir.y = 0;
            dir.Normalize();
            animator.SetFloat("Walking", dir.magnitude);
            rb.AddForce(dir * accelerationSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    private void RotationWalkChange()
    {
        if (dir.magnitude != 0 && isWalking)
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
