using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingTurretScript : MonoBehaviour
{
    public static DroppingTurretScript Instance;
    public Transform selectedGrid;
    public bool isDropping;
    public GameObject ball;

    public Transform firstSelectedTurret;
    public TurretInfo selectedTurret;
    public GameObject showTurretClone;
    public LayerMask highLightLayer;

    public GameObject holdingThisTurret;
    public bool isHoldingTurret;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        selectedTurret = firstSelectedTurret.GetComponent<InventoryScript>().turret;
    }

    private void Update()
    {
        CheckIfTurretCanLandHere();

        if (isHoldingTurret)
        {
            PickUpTurret(holdingThisTurret);
        }

    }

    public void TurretChange(TurretInfo turret)
    {
        selectedTurret = turret;
        Destroy(showTurretClone);
    }

    private void CheckIfTurretCanLandHere()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f, highLightLayer))
        {
            if (selectedGrid != null & hit.transform != selectedGrid)
            {
                selectedGrid.GetComponent<GridTile>().LightDown();
            }
            selectedGrid = hit.transform;
            selectedGrid.GetComponent<GridTile>().LightUp();

            if (showTurretClone == null)
            {
                showTurretClone = Instantiate(selectedTurret.showTurret);
            }
            showTurretClone.transform.position = hit.transform.position;
            showTurretClone.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);

            HandleTurretPlacement(hit);
        }
        else
        {
            if (selectedGrid != null)
            {
                PutTileOff();
            }
            
        }

    }

    private void PutTileOff()
    {
        selectedGrid.GetComponent<GridTile>().LightDown();
        selectedGrid = null;
        Destroy(showTurretClone);
    }
    private void HandleTurretPlacement(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!hit.transform.GetComponent<GridTile>().turretAlreadyHere & selectedTurret.amountOfThisTurret > 0)
            {
                DropTurret(hit.transform);
            }

            else if (hit.transform.GetComponent<GridTile>().turretAlreadyHere & hit.transform.GetComponent<GridTile>().turret != null)
            {
                holdingThisTurret = hit.transform.GetComponent<GridTile>().turret;
                holdingThisTurret.GetComponentInChildren<BaseTurret>().attackPower /= 2;
                hit.transform.GetComponent<GridTile>().turretAlreadyHere = false;
                isHoldingTurret = true;
            }
        }
    }
    private void DropTurret(Transform gridDropPlace)
    {
        if (isHoldingTurret)
        {
            gridDropPlace.GetComponent<GridTile>().MakeTheTurretHere();
            isHoldingTurret = false;
            holdingThisTurret.transform.position = new Vector3(gridDropPlace.position.x, holdingThisTurret.transform.position.y, gridDropPlace.position.z);
            holdingThisTurret.AddComponent<Rigidbody>();
            holdingThisTurret.GetComponentInChildren<BaseTurret>().attackPower *= 2;           
        }
        else
        {
            selectedTurret.amountOfThisTurret--;
            gridDropPlace.GetComponent<GridTile>().MakeTheTurretHere();
            Instantiate(ball, new Vector3(gridDropPlace.position.x, transform.position.y, gridDropPlace.position.z), transform.rotation);
        }
        
    }

    private void PickUpTurret(GameObject turret)
    {
        turret.transform.position = Vector3.MoveTowards(turret.transform.position, transform.position, 0.5f);
        
    }
}
