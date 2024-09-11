using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HoneyShooterBehaviour : MonoBehaviour
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
        lowestIndex = int.MaxValue;
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
            AttackEnemy();
        }
    }

    private void ChecksForEnemy()
    {
        transform.Rotate(0, speed, 0);        
    }

    private void SeesEnemy()
    {
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
                Instantiate(bullet, transform.position, transform.rotation);
                yield return new WaitForSeconds(3f);
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
            lowestIndex = int.MaxValue;           
            SeesEnemy();
        }
    }
}

