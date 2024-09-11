using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HoneyShooterBehaviour : InheritTurretStats
{
    public Transform enemyFolder;

    public float range;
    public List<Transform> enemiesSeen;
    public float speed;

    private Transform enemyToAttack;
    private int lowestIndex;

    public GameObject bullet;

    private void Start()
    {
        enemyFolder = GameObject.Find("EnemyFolder").transform;
        StartCoroutine(Shooting());
    }
    private void Update()
    {
        if (enemiesSeen.Count == 0)
        {
            ChecksForEnemy();
        }
        else
        {
            CheckIfEnemyAlife();
            AttackEnemy();
        }
    }

    private void CheckIfEnemyAlife()
    {
        enemiesSeen.RemoveAll(GameObject => GameObject == null);

        if (enemiesSeen.Count != 0)
        {
            SeesEnemy();
        }
        
    }
    private void ChecksForEnemy()
    {
        transform.Rotate(0, speed, 0);        
    }

    private void SeesEnemy()
    {
        lowestIndex = int.MaxValue;
        foreach (Transform enemy in enemiesSeen)
        {
            int index = enemy.GetSiblingIndex();

            if (index < lowestIndex)
            {
                lowestIndex = index;
            }
        }

        enemyToAttack = enemyFolder.GetChild(lowestIndex);
    }

    private void AttackEnemy()
    {
        if (enemyToAttack == null) return;
        Vector3 rotationToLook = enemyToAttack.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(rotationToLook);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            yield return null;
            while (enemiesSeen.Count > 0)
            {
                GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
                bulletClone.GetComponent<BallMovement>().attackPower = attackPower;
                yield return new WaitForSeconds(0.5f);
            }          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesSeen.Add(other.transform);
            SeesEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesSeen.Remove(other.transform);
            
            if (enemiesSeen.Count != 0)
            {
                SeesEnemy();
            }
            
        }
    }
}

