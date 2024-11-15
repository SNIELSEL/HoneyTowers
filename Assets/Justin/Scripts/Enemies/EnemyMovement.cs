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

    public Transform endPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyStats.speed;

        targetFolder = GameObject.Find("EnemyPath").transform;
        for (int i = 0; i < targetFolder.childCount; i++)
        {
            targets.Add(targetFolder.GetChild(i).transform);
        }
        endPoint = GameObject.Find("EndPoint").transform;
        agent.destination = targets[currentIndex].position;

    }
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (currentIndex < targets.Count && Vector3.Distance(transform.position, targets[currentIndex].position) < 0.01f)
        {
            currentIndex++;
        }

        if (currentIndex < targets.Count)
        {
            agent.destination = targets[currentIndex].position;
        }
        else
        {
            agent.destination = endPoint.position;
        }
    }
}
