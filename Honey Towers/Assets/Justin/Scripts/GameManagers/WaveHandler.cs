using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EnemyLayouts
{
    NormalOrder,
    Random
}
[Serializable]
public struct EnemyGroups
{
    public string groupName;
    public List<GameObject> availableEnemies;
    public int minScoreCost;
    public int maxScoreCost;
    public int enemyCount;
    public float enemyInterval;
    public EnemyLayouts enemyLayouts;

}
public class WaveHandler : MonoBehaviour
{
    public static WaveHandler Instance;

    public List<EnemyGroups> allGroups;
    public List<EnemyGroups> availableGroups;
    public EnemyGroups chosenEnemyGroup;
    public int score = 0;

    public float enemyHpMultiplier;
    public float enemySpeedMultiplier;

    public Transform enemyFolder;
    public Transform startPoint;
    public List<GameObject> enemiesSpawned;

    public float waveInterval;

    public TMP_Text battleText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void StartWave()
    {    
        ChooseGroup();
        EditGroupValues();
        switch (chosenEnemyGroup.enemyLayouts)
        {
            case EnemyLayouts.NormalOrder:
                StartCoroutine(HandleNormalOrder());
                break;
            case EnemyLayouts.Random:
                StartCoroutine(HandleRandomOrder());
                break;
                
        }      
    }

    private void ChooseGroup()
    {
        availableGroups.Clear();
        foreach (var group in allGroups)
        {
            if (score >= group.minScoreCost && score <= group.maxScoreCost)
            {
                availableGroups.Add(group);
            }
        }
        chosenEnemyGroup = availableGroups[UnityEngine.Random.Range(0, availableGroups.Count)];
    }

    private void EditGroupValues()
    {
        score++;   
    }


    private IEnumerator HandleNormalOrder()
    {
        int chosenEnemy = 0;
        for (int i = 0; i < chosenEnemyGroup.enemyCount; i++)
        {           
            GameObject enemy = Instantiate(chosenEnemyGroup.availableEnemies[chosenEnemy],
                startPoint.position, Quaternion.identity, enemyFolder);
            enemiesSpawned.Add(enemy);

            chosenEnemy++;
            if (chosenEnemy >= chosenEnemyGroup.availableEnemies.Count)
            {
                chosenEnemy = 0;
            }           
            yield return new WaitForSeconds(chosenEnemyGroup.enemyInterval);
        }
        GameManager.Instance.isWaveStarted = true;

    }

    private IEnumerator HandleRandomOrder()
    {
        for (int i = 0; i < chosenEnemyGroup.enemyCount; i++)
        {
            GameObject enemy = Instantiate(chosenEnemyGroup.availableEnemies[UnityEngine.Random.Range(0, chosenEnemyGroup.availableEnemies.Count)],
                startPoint.position, Quaternion.identity, enemyFolder);
            enemiesSpawned.Add(enemy);
            yield return new WaitForSeconds(chosenEnemyGroup.enemyInterval);
        }
    }

    public IEnumerator HandleWaveLength()
    {
        StartCoroutine(TextChanging());
        yield return new WaitForSeconds(waveInterval);
        StartWave();
    }
    public void EnemyDie(GameObject enemy)
    {
        enemiesSpawned.Remove(enemy);
    }

    private IEnumerator TextChanging()
    {
        float time = waveInterval;

        while (time > 0)
        {
            time -= Time.deltaTime;
            battleText.text = "Next wave is starting in " + (int)time;
            yield return null;
        }

        battleText.text = "Wave " + GameManager.Instance.waveNumber + " is starting";
    }
}
