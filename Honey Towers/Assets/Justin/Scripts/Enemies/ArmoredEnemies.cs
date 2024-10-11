using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemies : EnemyBehaviour
{
    public string tagToGetHit;
    public override void TakeHP(int damage, Transform objectToHit)
    {
        if (objectToHit.tag != tagToGetHit) return;
        base.TakeHP(damage, objectToHit);
    }
}
