using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeeHive : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public Slider hpBar;
    public AudioSource damageSound;
    public GameObject damageNumberPrefab;
    public Transform damageNumberSpawnPlace;

    private void Awake()
    {
        hpBar.maxValue = maxHp;
        hpBar.value = hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hp > 0)
            {
                HandleLosingHP(other.gameObject);
            }

            Destroy(other.gameObject);

        }
    }

    private void HandleLosingHP(GameObject enemy)
    {
        hp -= enemy.GetComponent<EnemyBehaviour>().attackPower;
        hpBar.value = hp;
        GameManager.Instance.CheckIfBeehiveDies(this);
        WaveHandler.Instance.EnemyDie(enemy);
        damageSound.Play();
        
        
    }

    /*private void ShowEnemyDamage(int damage)
    {
        GameObject damageNumber = Instantiate(damageNumberPrefab, damageNumberSpawnPlace.position, Quaternion.identity, transform);

        if (hp <= 0)
        {
            damageNumber.transform.SetParent(transform.parent);
        }
        damageNumber.GetComponentInChildren<TMP_Text>().text = "-" + damage.ToString();
    }*/




}
