using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Turret")]
public class TurretStats : ScriptableObject
{
    public int attackPower;
    public float intervalSpeed;
}
