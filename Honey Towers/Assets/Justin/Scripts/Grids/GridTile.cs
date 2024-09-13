using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{

    public bool turretAlreadyHere;
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
            Instantiate(PlayerStats.Instance.selectedTurret.turret, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
