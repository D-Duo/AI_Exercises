using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perception : MonoBehaviour
{
    public Zombie_Manager zombie_Manager;
    public NavMeshAgent agent;

    [Range(0, 100)] public float speed = 5f;
    [Range(1, 500)] public float walkRadius = 10f;
    [Range(0, 100)] public int maxRestingTime = 5;

    int restingTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if ()
        //{ }
        //else
            Wander();
    }

    void Wander()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 destination = Vector3.zero;
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1))
            {
                destination = hit.position;
            }

            if (restingTime == 0)
            {
                Seek(destination);
                restingTime = Random.Range(0, maxRestingTime);
            }
            else if (restingTime >= 0)
            {
                restingTime--;
            }
        }
    }

    void Seek(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
