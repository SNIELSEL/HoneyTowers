using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public TurretInfo turret;
    public void ChangingTurret()
    {
        DroppingTurretScript.Instance.TurretChange(turret);
    }

    public void AmountGoesUp()
    {
        turret.turretAmount++;
    }

    public void SelectedColor(Image image)
    {
        image.color = Color.red;
    }
    
    public void DeSelectedColor(Image image)
    {
        image.color = Color.white;
    }
}
