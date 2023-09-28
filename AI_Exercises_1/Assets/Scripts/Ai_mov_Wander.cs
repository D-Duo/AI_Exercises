using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_mov_Wander : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    [Range(0, 100)] public float speed = 0.5f;
    [Range(1, 500)] public float walkRadius = 50f;
    [Range(0, 100)] public int maxRestingTime = 5;

    float freq = 0f;
    int restingTime = 0;

    void Start()
    {
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            if (agent != null) { Wander(); }
        }
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

            if(restingTime == 0)
            {
                agent.SetDestination(destination);
                restingTime = Random.Range(0, maxRestingTime);
            }
            else if(restingTime >= 0)
            {
                restingTime--;
            }
        }
    }
}
