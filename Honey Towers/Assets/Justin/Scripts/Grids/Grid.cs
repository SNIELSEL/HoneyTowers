using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject turret;
    public void LightUp()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void LightDown()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Instantiate(turret, transform.position, Quaternion.identity);
        }
    }
}
