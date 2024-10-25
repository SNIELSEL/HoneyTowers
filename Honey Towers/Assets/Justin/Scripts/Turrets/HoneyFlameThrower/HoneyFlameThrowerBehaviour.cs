using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class HoneyFlameThrowerBehaviour : BaseTurret
{
    public float distance;
    public LayerMask enemy;
    public ParticleSystem flames;
    public bool seesEnemy;
    public AudioSource flameSound;


    protected override void Start()
    {
        base.Start();
        flames = GetComponent<ParticleSystem>();

    }
    protected override void Update()
    {
        CheckIfEnemyAlive();
        
        if (enemiesSeen.Count > 0 && !seesEnemy)
        {
            flames.Play();
            flameSound.Play();
            seesEnemy = true;
        }

        else if (enemiesSeen.Count == 0 && seesEnemy)
        {
            flames.Stop();
            flameSound.Stop();
            seesEnemy = false;
        }
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
