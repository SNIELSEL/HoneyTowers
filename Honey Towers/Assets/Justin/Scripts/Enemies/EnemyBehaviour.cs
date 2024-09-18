using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public int hp;
    public float speed;
    public int attackPower;

    public Slider hpBar;
    public EnemyStats stats;

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

    public void ShowEnemyDamage(int damage)
    {
        
    }

    
}