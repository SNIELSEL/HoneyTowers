using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform enemyFolder;

    public float range;
    public List<Transform> enemiesSeen;
    public float speed;

    public float rotateSpeed;
    public float targetEnemySpeed;
    protected Transform enemyToAttack;
    protected int lowestIndex;

    public GameObject bullet;
    public TurretStats turretStats;

    public int attackPower;
    public float intervalTime;
    public int price;

    protected float time;

    public float upgradePoints;
    public float maxUpgradePoint;
    public TMP_Text upgradeText;

    public Transform spawnPoint;

    protected virtual void Awake()
    {
        attackPower = turretStats.attackPower;
        intervalTime = turretStats.intervalSpeed;
        price = turretStats.price;
    }

    protected virtual void Start()
    {
        time = intervalTime;

        enemyFolder = GameObject.Find("EnemyFolder").transform;
    }

    protected virtual void Update()
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
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
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
        GameObject bulletClone = Instantiate(bullet, spawnPoint.position, transform.rotation, transform.parent);
        bulletClone.GetComponent<BaseBullet>().attackPower = attackPower;
        bulletClone.GetComponent<BaseBullet>().ObjectToGet(enemyToAttack);
    }

    protected virtual void LookAtEnemy()
    {
        if (enemyToAttack == null) return;
        Vector3 directionToEnemy = (enemyToAttack.position - transform.position).normalized;
        directionToEnemy.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, targetEnemySpeed * Time.deltaTime);
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesSeen.Add(other.transform);
            SeesEnemy();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
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

    public virtual void UpgradePoints()
    {
        upgradePoints++;

        
        if (upgradePoints >= maxUpgradePoint)
        {
            UpgradeTurret();
        }

        upgradeText.text = "Next upgrade: " + upgradePoints.ToString() + "/" + maxUpgradePoint.ToString();
    }

    protected virtual void UpgradeTurret()
    {
        attackPower *= 2;
        intervalTime /= 2;

        maxUpgradePoint *= 2;
        upgradePoints = 0;
    }

    


}
