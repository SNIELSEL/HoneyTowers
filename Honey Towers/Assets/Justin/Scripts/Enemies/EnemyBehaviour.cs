using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public int hp;
    public float speed;
    public int attackPower;

    public Slider hpBar;
    public EnemyStats stats;

    public Transform damageNumberSpawnPlace;
    public GameObject damageNumberPrefab;

    private GameObject damageNumberClone;

    private int totalDamageNumber;

    private void Awake()
    {
        hp = stats.hp;
        speed = stats.speed;
        attackPower = stats.attackPower;

        hpBar.maxValue = stats.hp;
        hpBar.value = stats.hp;
    }

    public void TakeHP(int damage)
    {
        hp -= damage;
        hpBar.value = hp;
        ShowEnemyDamage(damage);
        
        if (hp <= 0)
        {
            WaveHandler.Instance.EnemyDie(gameObject);
            PlayerStats.Instance.GetCoins(5);
            Destroy(gameObject);
        }
    }

    private void ShowEnemyDamage(int damage)
    {
        if (damageNumberClone == null)
        {
            damageNumberClone = Instantiate(damageNumberPrefab, damageNumberSpawnPlace.position, Quaternion.identity, transform);
            totalDamageNumber = 0;
        }

        totalDamageNumber += damage;
        if (hp <= 0)
        {
            damageNumberClone.transform.SetParent(transform.parent);
        }
        damageNumberClone.GetComponentInChildren<TMP_Text>().text = "-" + totalDamageNumber.ToString();
    }

    
}