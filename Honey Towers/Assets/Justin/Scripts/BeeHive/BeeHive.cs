using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeHive : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public Slider hpBar;

    private void Awake()
    {
        hpBar.maxValue = maxHp;
        hpBar.value = hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HandleLosingHP(other.gameObject);
        }
    }

    private void HandleLosingHP(GameObject enemy)
    {
        hp -= enemy.GetComponent<EnemyBehaviour>().attackPower;
        hpBar.value = hp;
        GameManager.Instance.CheckIfBeehiveDies(this);
        WaveHandler.Instance.EnemyDie(enemy);
        Destroy(enemy);
    }

    

    
}
