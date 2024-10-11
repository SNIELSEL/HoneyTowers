using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisObject : MonoBehaviour
{
    public float livingTime;

    private void Start()
    {
        Destroy(gameObject, livingTime);
    }
}
