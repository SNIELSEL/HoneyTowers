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

    public Transform spawnPoint;

    public int timesUpgraded;
    public int maxUpgradeTimes;
    public TMP_Text attackPowerText, intervalTimeText, upgradeText;

    public AudioSource shootingSound;

    protected virtual void Awake()
    {
        CheckTurretStats.Instance.AddTurret(transform.parent.gameObject);
        attackPower = turretStats.attackPower;
        intervalTime = turretStats.intervalSpeed;
        price = turretStats.price;
        maxUpgradeTimes = 2;
        attackPowerText.text = "AttackPower: " + attackPower;
        intervalTimeText.text = "IntervalTime: " + intervalTime;
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
        shootingSound.Play();
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
            timesUpgraded++;
        }

        if (timesUpgraded >= maxUpgradeTimes)
        {
            upgradeText.text = "Max upgraded!";
        }

        else
        {
            upgradeText.text = "Next upgrade: " + upgradePoints.ToString() + "/" + maxUpgradePoint.ToString();
        }

        upgradeText.text = "UpgradePoints: " + upgradePoints + "/" + maxUpgradePoint;


    }

    protected virtual void UpgradeTurret()
    {
        attackPower *= 2;
        intervalTime /= 2;
        maxUpgradePoint *= 2;
        upgradePoints = 0;
        attackPowerText.text = "AttackPower: " + attackPower;
        intervalTimeText.text = "IntervalTime: " + intervalTime;
    }

    


}
