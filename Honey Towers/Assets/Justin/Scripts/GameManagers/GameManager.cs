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

    public PlayableDirector winDirector;
    public PlayableDirector loseDirector;
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
        loseDirector.Play();
        WaveHandler.Instance.StopAllCoroutines();
        WaveHandler.Instance.enabled = false;
        WaveHandler.Instance.battleText.enabled = false;
        this.enabled = false;
    }

    private void HandleWin()
    {
        if (waveNumber > maxWaves)
        {
            winDirector.Play();
            WaveHandler.Instance.StopAllCoroutines();
            WaveHandler.Instance.enabled = false;
            WaveHandler.Instance.battleText.enabled = false;
            this.enabled = false;
        }
    }

}
