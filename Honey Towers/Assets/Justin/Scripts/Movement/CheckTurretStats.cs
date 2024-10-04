using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckTurretStats : MonoBehaviour
{
    public static CheckTurretStats Instance;
    public List<GameObject> turrets = new List<GameObject>();
    public LayerMask turretMask;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, turretMask))
            {
                foreach (GameObject turret in turrets)
                {
                    if (turret == hit.transform.gameObject)
                    {
                        turret.GetComponentInChildren<Canvas>().enabled = true;
                    }
                    else
                    {
                        turret.GetComponentInChildren<Canvas>().enabled = false;
                    }
                }  
            }
            else
            {
                foreach (GameObject turret in turrets)
                {
                    turret.GetComponentInChildren<Canvas>().enabled = false;
                }
            }
        }

        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayCast, out RaycastHit hitCast, Mathf.Infinity, turretMask))
        {
            

        }
    }

    public void AddTurret(GameObject turret)
    {
        turrets.Add(turret);
    }

    public void RemoveTurret(GameObject turret)
    {
        turrets.Remove(turret);
    }
}
