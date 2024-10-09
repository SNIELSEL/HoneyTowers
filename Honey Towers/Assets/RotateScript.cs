using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
