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

    private void Bullshit()
    {
        int epic = 0;

        for (int i = 0; i < 10; i++)
        {
            if (epic == 0)
            {
                break;
            }
        }
    }
}
