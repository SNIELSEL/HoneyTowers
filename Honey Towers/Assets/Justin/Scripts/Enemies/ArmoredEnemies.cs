using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemies : EnemyBehaviour
{
    public List<string> tagsToGetHit;
    public override void TakeHP(int damage, Transform objectToHit)
    {
        if (!tagsToGetHit.Contains(objectToHit.tag)) return;
        base.TakeHP(damage, objectToHit);
    }
}
