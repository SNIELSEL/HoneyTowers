using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TMP_Text gameOverText;

    public bool isWaveStarted;
    public int waveNumber;

    public PlayableDirector winDirector;
    public PlayableDirector loseDirector;

    public AudioSource winSound, loseSound, normalMusic;
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
            StartCoroutine(WaveHandler.Instance.HandleWaveLength(WaveHandler.Instance.waveInterval));
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
        gameOverText.text = "Made it to wave " + waveNumber;
        MovementScript.Instance.enabled = false;
        CameraMovement.Instance.enabled = false;
        OpenMenu.Instance.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        loseSound.Play();
        normalMusic.Stop();


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

            MovementScript.Instance.enabled = false;
            CameraMovement.Instance.enabled = false;
            OpenMenu.Instance.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            winSound.Play();
            normalMusic.Stop();
            this.enabled = false;
        }
    }

}
