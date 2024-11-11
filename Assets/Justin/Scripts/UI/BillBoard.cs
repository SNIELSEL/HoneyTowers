using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
