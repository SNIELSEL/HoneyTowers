using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class HoneyShooterBehaviour : InheritTurretStats
{
    public TurretStats turretStats;
    public Transform enemyFolder;

    public float range;
    public List<Transform> enemiesSeen;
    public float speed;

    public float rotateSpeed;
    private Transform enemyToAttack;
    private int lowestIndex;

    public GameObject bullet;

    private float time;


    private void Start()
    {
        enemyFolder = GameObject.Find("EnemyFolder").transform;
    }
    private void Update()
    {
        if (enemiesSeen.Count == 0)
        {
            PutCountDownToZero();
            ChecksForEnemy();
        }
        else
        {
            TimeUp();
            CheckIfEnemyAlife();
            AttackEnemy();
        }
    }

    private void PutCountDownToZero()
    {
        time = 0;
    }

    private void TimeUp()
    {
        time += Time.deltaTime;
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
        Vector3 directionToEnemy = (enemyToAttack.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

        if (time >= turretStats.intervalSpeed & Quaternion.Angle(transform.rotation, lookRotation) < 4f)
        {
            ShootAtEnemy();
            time = 0;
        }
    }

    private void ShootAtEnemy()
    {
        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation, transform.parent);
        bulletClone.GetComponent<BallMovement>().attackPower = attackPower;
        bulletClone.GetComponent<BallMovement>().ObjectToGet(enemyToAttack);
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

