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
    public RaycastHit hit;
    private Transform previousTurret;
    private bool isRaycasting;
    private bool hoveringTurret;
    private Ray ray;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
       /* ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hoveringTurret = Physics.Raycast(ray, out hit, Mathf.Infinity, turretMask);
        if (Input.GetMouseButtonDown(0))
        {

            if (hoveringTurret)
            {
                foreach (GameObject turret in turrets)
                {
                    if (turret == hit.transform.parent.gameObject)
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

        if (hoveringTurret)
        {
            if (previousTurret != null && hit.transform.parent != previousTurret)
            {
                foreach (Renderer renderer in previousTurret.GetComponentsInChildren<Renderer>())
                {
                    foreach (Material material in renderer.materials)
                    {
                        material.color = Color.white;
                    }
                }
                
            }
            previousTurret = hit.transform.parent;
            isRaycasting = true;

            foreach (Renderer renderer in hit.transform.parent.GetComponentsInChildren<Renderer>())
            {
                foreach (Material material in renderer.materials)
                {
                    material.color = Color.red;
                }
                
            }

        }

        if (isRaycasting && !hoveringTurret)
        {
            foreach (Renderer renderer in previousTurret.GetComponentsInChildren<Renderer>())
            {
                foreach (Material material in renderer.materials)
                {
                    material.color = Color.white;
                }
            }

            isRaycasting = false;
        }
       */
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
