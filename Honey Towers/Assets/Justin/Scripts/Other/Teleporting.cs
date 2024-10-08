using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Teleporting : MonoBehaviour
{
    public Transform newPosition;
     private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Enemy"))
        {
            other.transform.GetComponent<NavMeshAgent>().Warp(newPosition.position);
            other.transform.GetComponent<EnemyMovement>().currentIndex++;
        }
    }
}
