using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int hp;
    public float speed;
    public int attackPower;

    public EnemyStats stats;

    private void Awake()
    {
        hp = stats.hp;
        speed = stats.speed;
        attackPower = stats.attackPower;
    }

    public void TakeHP(int damage)
    {
        hp -= damage;
        
        if (hp <= 0)
        {
            WaveHandler.Instance.EnemyDie(gameObject);
            PlayerStats.Instance.GetCoins(5);
            Destroy(gameObject);
        }
    }

}