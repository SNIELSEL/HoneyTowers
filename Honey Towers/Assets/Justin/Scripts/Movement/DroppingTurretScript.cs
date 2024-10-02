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
        if (MovementScript.Instance.isWalking)
        {
            if (showTurretClone != null)
            {
                Destroy(showTurretClone);
                selectedGrid.GetComponent<GridTile>().LightDown();
            }

            return;
        }
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
            HandleColorOfTile();

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

    private void HandleColorOfTile()
    {
        if (selectedGrid.GetComponent<GridTile>().turretAlreadyHere)
        {
            selectedGrid.GetComponent<GridTile>().LightUp(Color.red);
        }
        else
        {
            selectedGrid.GetComponent<GridTile>().LightUp(Color.green);
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
            if (selectedTurret.turretAmount > 0 || holdingThisTurret != null)
            {
                DropTurret(hit.transform);
            }         
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hit.transform.GetComponent<GridTile>().turretAlreadyHere & hit.transform.GetComponent<GridTile>().turret != null)
            {
                GetComponentInChildren<Animator>().SetTrigger("PutBack");
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
            if (gridDropPlace.GetComponent<GridTile>().turretAlreadyHere) return;
            GetComponentInChildren<Animator>().SetTrigger("PutFront");
            gridDropPlace.GetComponent<GridTile>().MakeTheTurretHere();
            isHoldingTurret = false;
            holdingThisTurret.transform.position = new Vector3(gridDropPlace.position.x, holdingThisTurret.transform.position.y, gridDropPlace.position.z);
            holdingThisTurret.transform.rotation = Quaternion.Euler(0, holdingThisTurret.transform.rotation.y, 0);
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
        float minY = GetComponent<Collider>().bounds.min.y - turret.transform.localScale.y;
        Vector3 newPos = new Vector3(transform.position.x, minY, transform.position.z);
        turret.transform.position = Vector3.MoveTowards(turret.transform.position, newPos, 0.5f);
        turret.transform.rotation = transform.rotation;      
    }

    
    
    
}
