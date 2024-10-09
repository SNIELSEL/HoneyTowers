using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoneyBee : MonoBehaviour
{
    public int attackPower;

    public NavMeshAgent agent;
    private Transform pathFolder;
    private List<Collider> pathColliders = new List<Collider>();

    private bool isReachingFirstPos;
    private Transform reachPointFolder;
    private List<Transform> reachPoints = new List<Transform>();

    private Vector3 endPosition;
    private int reachPointIndex;

    private Transform startPoint;

    private void Awake()
    {
        isReachingFirstPos = true;
        agent = GetComponent<NavMeshAgent>();
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
    }

    private void Start()
    {
        FindPath();
        endPosition = SearchFirstPosition();
        agent.SetDestination(endPosition);
    }

    private void FindPath()
    {
        pathFolder = GameObject.FindGameObjectWithTag("PathFolder").transform;
        reachPointFolder = GameObject.Find("EnemyPath").transform;

        for (int i = 0; i < reachPointFolder.childCount; i++)
        {
            reachPoints.Add(reachPointFolder.GetChild(i).GetComponent<Transform>());
        }

        for (int i = 0; i < pathFolder.childCount; i++)
        {
            pathColliders.Add(pathFolder.GetChild(i).GetComponent<Collider>());
        }
    }

    private Vector3 SearchFirstPosition()
    {
        Vector3 position = Vector3.zero;
        float smallestDistance = Mathf.Infinity;

        foreach (Collider collider in pathColliders)
        {
            Vector3 closestPoint = collider.ClosestPoint(transform.position);
            float distance = Vector3.Distance(closestPoint, transform.position);

            if (distance < smallestDistance) 
            {
                smallestDistance = distance;
                position = closestPoint;
            }

        }

        return position;
    }
    private void Update()
    {
        if (isReachingFirstPos && Vector3.Distance(transform.position, endPosition) < 0.6f)
        {
            FindRightReachPoint();
        }
        
        else if (!isReachingFirstPos && Vector3.Distance(transform.position, endPosition) < 0.6f)
        {
            MoveToNextReachPoint();
        }
    }

    private void FindRightReachPoint()
    {
        Transform closestObjectA = null;
        Transform closestObjectB = null;
        float minDistanceA = Mathf.Infinity;
        float minDistanceB = Mathf.Infinity;
        foreach (var reachPoint in reachPoints)
        {
            float distance = Vector3.Distance(transform.position, reachPoint.position);

            if (distance < minDistanceA)
            {
                minDistanceB = minDistanceA;
                closestObjectB = closestObjectA;

                minDistanceA = distance;
                closestObjectA = reachPoint;
            }
            else if (distance < minDistanceB)
            {
                minDistanceB = distance;
                closestObjectB = reachPoint;
            }
        }

        int indexChildA = closestObjectA.GetSiblingIndex();
        int indexChildB = closestObjectB.GetSiblingIndex();

        if (indexChildA < indexChildB)
        {
            agent.SetDestination(closestObjectA.position);
            endPosition = closestObjectA.position;
            reachPointIndex = indexChildA;
        }
        else
        {
            agent.SetDestination(closestObjectB.position);
            endPosition = closestObjectB.position;
            reachPointIndex = indexChildB;
        }

        isReachingFirstPos = false;
    }

    private void MoveToNextReachPoint()
    {
        if (reachPointIndex > 0)
        {
            NextWavePoint();
        }
        else
        {
            agent.SetDestination(startPoint.position);
            endPosition = startPoint.position;
        }
        
    }

    public void NextWavePoint()
    {
        print("WORK");
        reachPointIndex--;
        agent.SetDestination(reachPoints[reachPointIndex].position);
        endPosition = reachPoints[reachPointIndex].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().TakeHP(attackPower);
            Destroy(gameObject);
        }

        if (other.transform == startPoint)
        {
            Destroy(gameObject);
        }
    }
}
