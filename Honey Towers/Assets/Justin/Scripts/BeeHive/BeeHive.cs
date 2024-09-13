using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour
{
    public int hp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hp -= other.GetComponent<EnemyBehaviour>().attackPower;
            GameManager.Instance.CheckIfBeehiveDies(this);
            WaveHandler.Instance.EnemyDie(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
