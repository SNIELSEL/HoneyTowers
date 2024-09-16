using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
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

    private void Awake()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputCheck();      
        Movement();
        RotationChange();
    }

    private void InputCheck()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        upAndDown = Input.GetAxisRaw("UpAndDown");
    }

    private void Movement()
    {
        currentSpeed = rb.velocity.magnitude;
        dir = cam.transform.forward * vert + cam.transform.right * hor + Vector3.up * upAndDown;
        dir.Normalize();
        rb.AddForce(dir * accelerationSpeed * Time.deltaTime, ForceMode.Acceleration);               
    }

    private void RotationChange()
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
}
