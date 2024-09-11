using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn;
    public Transform startPoint;
    public int maxWaves;

    private int chanceOfHarderEnemy;
    private int waveNumber = 1;

    private void Start()
    {
        
    }

    private void StartWave()
    {
        StartCoroutine(StartSpawningEnemies());
    }

    private IEnumerator StartSpawningEnemies()
    {
        int amountOfEnemies = 10;

        for (int i = 0; i < amountOfEnemies; i++)
        {
            Instantiate(enemiesToSpawn[0], startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }


}
