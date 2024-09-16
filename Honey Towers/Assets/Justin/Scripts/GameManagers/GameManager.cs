using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
    public int waveNumber;

    public PlayableDirector playableDirector;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    private void Update()
    {
        HandleWin();
        if (isWaveStarted)
        {
            CheckIfEnemiesDie();
        }
    }

    private void CheckIfEnemiesDie()
    {
        if (WaveHandler.Instance.enemiesSpawned.Count == 0 && isWaveStarted)
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

    private void HandleWin()
    {
        if (waveNumber > maxWaves)
        {
            playableDirector.Play();
            this.enabled = false;
        }
    }

}
