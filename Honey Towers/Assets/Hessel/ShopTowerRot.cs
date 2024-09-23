using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTowerRot : MonoBehaviour
{
    public float rotateSpeed;
    public bool isShopping;

    private void Update()
    {
        if (isShopping)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
}
