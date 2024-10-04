using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBeeSpawner : BaseTurret
{
    public GameObject bee;
    public Transform beeSpawnPlace;
    public int beeAmount;

    private void OnEnable()
    {
        StartCoroutine(SpawnBees());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnBees()
    {
        while (true)
        {
            for (int i = 0; i < beeAmount; i++)
            {
                GameObject beeClone = Instantiate(bee, beeSpawnPlace.position, Quaternion.identity, transform);
                beeClone.GetComponent<HoneyBee>().attackPower = attackPower;
                yield return new WaitForSeconds(0.1f);
            }
            
            yield return new WaitForSeconds(intervalTime);
        }
    }

}
