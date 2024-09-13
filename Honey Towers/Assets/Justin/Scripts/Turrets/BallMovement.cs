using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public int attackPower;

    private Transform objToChase;
    private void Update()
    {
        if (objToChase != null)
        {
            Vector3 directionToEnemy = (objToChase.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 50 * Time.deltaTime);
            
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void ObjectToGet(Transform obj)
    {
        objToChase = obj;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().TakeHP(attackPower);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == transform.parent)
        {
            Destroy(gameObject);
        }
    }
}
