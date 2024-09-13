using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingTurretScript : MonoBehaviour
{
    public Transform selectedGrid;
    public bool isDropping;
    public GameObject ball;

    public LayerMask highLightLayer;
    
    private void Update()
    {
        TurretCanLandHere();            
    }

    private void TurretCanLandHere()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, highLightLayer))
        {
            if (selectedGrid != null & hit.transform != selectedGrid)
            {
                selectedGrid.GetComponent<GridTile>().LightDown();
            }
            selectedGrid = hit.transform;
            selectedGrid.GetComponent<GridTile>().LightUp();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Drop(hit.transform);
            }
        }
    }
    private void Drop(Transform gridDropPlace)
    {
        if (!gridDropPlace.GetComponent<GridTile>().turretAlreadyHere & PlayerStats.Instance.selectedTurret.amountOfThisTurret > 0)
        {
            PlayerStats.Instance.selectedTurret.amountOfThisTurret--;
            gridDropPlace.GetComponent<GridTile>().MakeTheTurretHere();
            Instantiate(ball, new Vector3(gridDropPlace.position.x, transform.position.y, gridDropPlace.position.z), Quaternion.identity);
        }
              
    }
}
