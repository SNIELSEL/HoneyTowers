using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Teleporting : MonoBehaviour
{
    public Transform newPosition;
    public string obj;
     private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag(obj))
        {
            other.transform.GetComponent<NavMeshAgent>().Warp(newPosition.position);
            if (other.CompareTag("Enemy"))
            {
                other.transform.GetComponent<EnemyMovement>().currentIndex++;
            }

            else
            {
                other.transform.GetComponent<HoneyBee>().NextWavePoint();
            }
            
        }
    }
}
