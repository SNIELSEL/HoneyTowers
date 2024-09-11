using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritTurretStats : MonoBehaviour
{
    public TurretStats turretStats;

    public int attackPower;
    public float intervalTime;

    private void Awake()
    {
        attackPower = turretStats.attackPower;
        intervalTime = turretStats.intervalSpeed;
    }
}
