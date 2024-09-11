using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public List<GameObject> enemiesToSpawn;
    public Transform startPoint;
    public int maxWaves;

    public Transform enemyFolder;
    private int chanceOfHarderEnemy;
    private int waveNumber = 1;

    private bool isWaveStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        StartWave();
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
            GameObject enemy = Instantiate(enemiesToSpawn[0], startPoint.position, Quaternion.identity, enemyFolder);
            enemiesToSpawn.Add(enemy);
            yield return new WaitForSeconds(1);
        }

        isWaveStarted = true;

    }

    private void Update()
    {
        if (isWaveStarted)
        {
            CheckIfEnemiesDie();
        }
    }

    private void CheckIfEnemiesDie()
    {
        if (enemiesToSpawn.Count == 0)
        {
            waveNumber++;
            StartSpawningEnemies();
        }
    }

    public void CheckIfBeehiveDies(BeeHive beehive)
    {
        if (beehive.hp <= 0)
        {
            this.enabled = false;
            print("You lose idiot haha");
        }
    } 

    public void EnemyDie(GameObject enemy)
    {
        enemiesToSpawn.Remove(enemy);
    }
}
