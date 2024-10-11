using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool turretAlreadyHere;
    public GameObject turret;
    public void LightUp(Color color)
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshRenderer>().material.color = color;
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
            if (turret == null)
            {
                turret = Instantiate(DroppingTurretScript.Instance.selectedTurret.turret, transform.position, other.transform.rotation);
            }
            else
            {
                turret.GetComponentInChildren<BaseTurret>().UpgradePoints();
            }

            other.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponentInChildren<AudioSource>().Play();
            other.transform.GetChild(0).parent = null;
            Destroy(other.gameObject);
            DroppingTurretScript.Instance.ballClone = null;
        }

        if (other.GetComponent<Rigidbody>() != null & other.CompareTag("Turret"))
        {
            turret = other.gameObject;
            other.transform.position = transform.position;

            if (other.GetComponentInChildren<HoneyBeeSpawner>() != null)
            {
                other.GetComponentInChildren<HoneyBeeSpawner>().enabled = true;
            }
            Destroy(other.GetComponent<Rigidbody>());

            
        }
    }
}
