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

    private bool tButton;
    private bool isFlyingSlow;

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
        FlyMovement();
        RotationFlyChange();
        ToggleSpeed();
    }

    private void InputCheck()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        upAndDown = Input.GetAxisRaw("UpAndDown");
        tButton = Input.GetKeyDown(KeyCode.T);
    }

    private void FlyMovement()
    {
        currentSpeed = rb.velocity.magnitude;
        dir = cam.transform.forward * vert + cam.transform.right * hor + Vector3.up * upAndDown;
        dir.Normalize();
        rb.AddForce(dir * accelerationSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

    private void RotationFlyChange()
    {
        if (dir.magnitude != 0)
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

    private void ToggleSpeed()
    {
        if (tButton && !isFlyingSlow)
        {
            isFlyingSlow = true;
            accelerationSpeed = 1000;
        }

        else if (tButton && isFlyingSlow)
        {
            isFlyingSlow = false;
            accelerationSpeed = 2000;
        }
    }
}
