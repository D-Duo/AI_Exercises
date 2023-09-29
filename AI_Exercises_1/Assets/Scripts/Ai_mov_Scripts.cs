using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_mov_Scripts : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject target;

    public string movScript = "none";
    [Range(0, 100)] public float speed = 0.5f;
    [Range(1, 500)] public float walkRadius = 50f;
    [Range(0, 100)] public int maxRestingTime = 5;

    float freq = 0f;
    int restingTime = 0;



    // Start is called before the first frame update
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
            if (agent != null) {
                switch (movScript)
                {
                    case "Wander":
                        Wander();
                        break;

                    case "Seek":
                        break;

                    default:
                        break;
                }
            }
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

            if (restingTime == 0)
            {
                agent.SetDestination(destination);
                restingTime = Random.Range(0, maxRestingTime);
            }
            else if (restingTime >= 0)
            {
                restingTime--;
            }
        }
    }

    void Seek()
    {
        agent.destination = target.transform.position;
    }
}
