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
            float roundToDegrees = Mathf.Round(transform.eulerAngles.y / 90) * 90;
            showTurretClone.transform.rotation = Quaternion.Euler(0, roundToDegrees, 0);

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
            if (selectedTurret.turretAmount > 0)
            {
                DropTurret(hit.transform);
            }         
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hit.transform.GetComponent<GridTile>().turretAlreadyHere & hit.transform.GetComponent<GridTile>().turret != null)
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
            holdingThisTurret = null;
        }
        else
        {
            selectedTurret.turretAmount--;
            selectedTurret.turretAmountText.text = selectedTurret.turretAmount.ToString();
            gridDropPlace.GetComponent<GridTile>().MakeTheTurretHere();

            float roundToDegrees = Mathf.Round(transform.eulerAngles.y / 90) * 90;
            Instantiate(ball, new Vector3(gridDropPlace.position.x, transform.position.y, gridDropPlace.position.z), Quaternion.Euler(0,roundToDegrees,0));
        }
        
    }

    private void PickUpTurret(GameObject turret)
    {
        turret.transform.position = Vector3.MoveTowards(turret.transform.position, transform.position, 0.5f);
        turret.transform.rotation = transform.rotation;      
    }

    
    
    
}
