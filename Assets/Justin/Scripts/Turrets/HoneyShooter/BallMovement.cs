using UnityEngine;

public class BallMovement : BaseBullet
{
    public GameObject tinyExplosion;

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
            other.GetComponent<EnemyBehaviour>().TakeHP(attackPower, transform);
            Instantiate(tinyExplosion, transform.position, Quaternion.identity);
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
