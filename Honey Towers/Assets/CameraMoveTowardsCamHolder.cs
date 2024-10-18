using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTowardsCamHolder : MonoBehaviour
{
    public Transform bee;
    public Transform camHolder;
    private Rigidbody rb;
    public float moveSpeed;

    public float minDistance;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, camHolder.position) < minDistance) return;
        rb.velocity = (camHolder.position - transform.position).normalized * moveSpeed;
        transform.LookAt(bee.position);
    }
}
