using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public int attackPower;
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void Update()
    {        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().TakeHP(attackPower);
            Destroy(gameObject);
        }
    }
}
