using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public List<GameObject> enemiesToSpawn;
    public Transform startPoint;
    public int maxWaves;

    public List<GameObject> enemiesSpawned;
    public Transform enemyFolder;

    public GameObject gameoverScreen;

    public bool isWaveStarted;
    private int waveNumber;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    /*private IEnumerator StartSpawningEnemies()
    {

        //for (int i = 0; i < ; i++)
        //{
        //    GameObject enemy = Instantiate(enemiesToSpawn[0], startPoint.position, Quaternion.identity, enemyFolder);
        //    enemiesSpawned.Add(enemy);
        //    yield return new WaitForSeconds(1);
        //}

        isWaveStarted = true;

    }*/

    private void Update()
    {
        if (isWaveStarted)
        {
            CheckIfEnemiesDie();
        }
    }

    private void CheckIfEnemiesDie()
    {
        if (enemiesSpawned.Count == 0 && isWaveStarted)
        {
            isWaveStarted = false;
            waveNumber++;
            StartCoroutine(WaveHandler.Instance.HandleWaveLength());
        }
    }

    public void CheckIfBeehiveDies(BeeHive beehive)
    {
        if (beehive.hp <= 0)
        {
            GameOverFunction();
        }
    } 

    private void GameOverFunction()
    {
        gameoverScreen.SetActive(true);
    }

}
