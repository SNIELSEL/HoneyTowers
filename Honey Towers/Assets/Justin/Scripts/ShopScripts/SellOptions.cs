using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellOptions : MonoBehaviour
{
    public void SellTurret()
    {
        if (DroppingTurretScript.Instance.holdingThisTurret != null)
        {
            PlayerStats.Instance.GetCoins(DroppingTurretScript.Instance.holdingThisTurret.GetComponentInChildren<BaseTurret>().price);
            Destroy(DroppingTurretScript.Instance.holdingThisTurret);
            DroppingTurretScript.Instance.isHoldingTurret = false;
            DroppingTurretScript.Instance.holdingThisTurret = null;
        }
    }
}
