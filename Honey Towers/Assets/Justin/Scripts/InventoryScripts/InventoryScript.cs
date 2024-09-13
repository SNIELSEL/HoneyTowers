using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public TurretInfo turret;
    public void ChangingTurret()
    {
        PlayerStats.Instance.TurretChange(turret);
    }

    public void AmountGoesUp()
    {
        turret.amountOfThisTurret++;
    }
}
