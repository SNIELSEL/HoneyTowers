using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemies : EnemyBehaviour
{
    public List<string> tagsToGetHit;
    public int damageTakenOff;
    public override void TakeHP(int damage, Transform objectToHit)
    {
        if (!tagsToGetHit.Contains(objectToHit.tag))
        {
            if (damageTakenOff == 0)
            {
                return;
            }

            else
            {
                damage /= damageTakenOff;
            }
        }
        base.TakeHP(damage, objectToHit);
    }
}
