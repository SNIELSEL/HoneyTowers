using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform enemyFolder;

    public float range;
    public List<Transform> enemiesSeen;
    public float speed;

    public float rotateSpeed;
    protected Transform enemyToAttack;
    protected int lowestIndex;

    public GameObject bullet;
    public TurretStats turretStats;

    public int attackPower;
    public float intervalTime;

    protected float time;

    protected virtual void Awake()
    {
        attackPower = turretStats.attackPower;
        intervalTime = turretStats.intervalSpeed;
    }

    protected void Start()
    {
        time = intervalTime;

        enemyFolder = GameObject.Find("EnemyFolder").transform;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (enemiesSeen.Count == 0)
        {
            CheckingForEnemy();
        }
        else
        {
            CheckIfEnemyAlive();
            LookAtEnemy();
            Reload();
        }
        
    }

    protected virtual void CheckingForEnemy()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    protected virtual void Reload()
    {

        if (time <= 0)
        {
            ShootAtEnemy();
            time = intervalTime;
        }
    }

    protected virtual void ShootAtEnemy()
    {
        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation, transform.parent);
        bulletClone.GetComponent<BaseBullet>().attackPower = attackPower;
        bulletClone.GetComponent<BaseBullet>().ObjectToGet(enemyToAttack);
    }

    protected virtual void LookAtEnemy()
    {
        if (enemyToAttack == null) return;
        Vector3 directionToEnemy = (enemyToAttack.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }

    protected virtual void SeesEnemy()
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

    protected void CheckIfEnemyAlive()
    {
        enemiesSeen.RemoveAll(GameObject => GameObject == null);

        if (enemiesSeen.Count != 0)
        {
            SeesEnemy();
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
