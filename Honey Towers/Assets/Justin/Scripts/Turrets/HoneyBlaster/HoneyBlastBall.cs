using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlastBall : BaseBullet
{
    
    public GameObject explosion;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().TakeHP(attackPower);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

   
}
