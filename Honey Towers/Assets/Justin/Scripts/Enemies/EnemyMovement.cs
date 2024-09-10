using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> targets;
    public Transform targetFolder;

    public EnemyStats enemyStats;
    public int currentIndex;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyStats.speed;
        for (int i = 0; i < targetFolder.childCount; i++)
        {
            targets.Add(targetFolder.GetChild(i).transform);
        }
    }
    private void Update()
    {
        agent.destination = targets[currentIndex].position;

        if (Vector3.Distance(transform.position, targets[currentIndex].position) < 0.01f)
        {
            currentIndex++;
        }
    }
}
