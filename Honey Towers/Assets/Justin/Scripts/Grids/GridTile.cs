using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{

    public bool turretAlreadyHere;
    public GameObject turret;
    public void LightUp()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void LightDown()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void MakeTheTurretHere()
    {
        turretAlreadyHere = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            turret = Instantiate(DroppingTurretScript.Instance.selectedTurret.turret, transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.GetComponent<Rigidbody>() != null & other.CompareTag("Turret"))
        {
            turret = other.gameObject;
            other.transform.position = transform.position;
            Destroy(other.GetComponent<Rigidbody>());
        }
    }
}
