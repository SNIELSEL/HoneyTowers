using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;  // The object the camera will rotate around
    public float distance = 10.0f;
    public float minDistance;
    public float maxDistance = 10.0f;// Distance between the camera and the object
    public float xSpeed = 120.0f;  // Horizontal rotation speed
    public float ySpeed = 120.0f;  // Vertical rotation speed

    private float x = 0.0f;
    private float y = 0.0f;

    public bool isMovingCamera;

    public LayerMask mapLayer;

    public float zoomSpeed;
    public float collisionOffset;

    public int layerNumber;
    public Collider mapCollider;

    public Transform cameraHolder;
    public float howMuchBeforeCamera;
    public float raycastLength;

    void Start()
    {
        // Initialize camera angles based on its current position
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

    }

    private void Update()
    {
        CheckIfRightMouseClick();
    }

    void LateUpdate()
    {
        CheckingForWall();
        MoveAndRotateCamera();
    }

    private void CheckIfRightMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            isMovingCamera = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            isMovingCamera = false;
        }
    }

    private void MoveAndRotateCamera()
    {
        if (target && isMovingCamera)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = Mathf.Clamp(y, -90f, 90f);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }

    private void CheckingForWall()
    {
        RaycastHit hit;

        Vector3 directionToCamera = transform.position - target.position;
        Debug.DrawRay(transform.position, directionToCamera * maxDistance, Color.green);
        if (Physics.Raycast(transform.position + transform.forward * howMuchBeforeCamera, directionToCamera.normalized, out hit, raycastLength, mapLayer))
        {
            float targetDistance = Mathf.Clamp(hit.distance - collisionOffset, minDistance, maxDistance);

            distance = Mathf.Lerp(distance, targetDistance, zoomSpeed * Time.deltaTime);

        }

        else
        {
            distance = Mathf.Lerp(distance, maxDistance, zoomSpeed * Time.deltaTime);
        }
    }
}
