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
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, highLightLayer))
        {
            if (selectedGrid != null & hit.transform != selectedGrid)
            {
                selectedGrid.GetComponent<Grid>().LightDown();
            }
            selectedGrid = hit.transform;
            selectedGrid.GetComponent<Grid>().LightUp();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Drop(hit.transform);
            }
        }

        

        
    }

    private void Drop(Transform gridDropPlace)
    {
        Instantiate(ball, new Vector3(gridDropPlace.position.x, transform.position.y, gridDropPlace.position.z), Quaternion.identity);       
    }
}
