using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FS_Machine : MonoBehaviour
{

    public NavMeshAgent agent;

    //wander

    [Range(0, 100)] public float speed = 5f;
    [Range(1, 500)] public float walkRadius = 10f;
    [Range(0, 100)] public int maxRestingTime = 5;
    int restingTime = 1;

    IEnumerator<int> fibonacci()
    {
        int a = 0;              // Output:
        int b = 1;              // 0,1,1,2,3,5,8,13,21,34...
        yield return a;
        while (true)
        {
            yield return b;
            int c = b;
            b = a + b;
            a = c;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator<int> f = fibonacci();
        for (int i = 0; i < 10; i++)
        {
            f.MoveNext();
            Debug.Log(f.Current);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
    }

    void Seek(Vector3 destination)
    {
        agent.SetDestination(destination);
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

}
