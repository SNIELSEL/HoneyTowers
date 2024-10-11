using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HoneyFlameThrowerBehaviour : BaseTurret
{
    public float distance;
    public LayerMask enemy;

    
    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {
        CheckIfEnemyAlive();
    }

    

    

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesSeen.Add(other.transform);
            StartCoroutine(SlowDamage(other.transform));
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesSeen.Remove(other.transform);
        } 
    }

    private IEnumerator SlowDamage(Transform enemy)
    {
        while (enemiesSeen.Contains(enemy))
        {
            enemy.GetComponent<EnemyBehaviour>().TakeHP(attackPower, transform);
            yield return new WaitForSeconds(intervalTime);
        }
    }

}
