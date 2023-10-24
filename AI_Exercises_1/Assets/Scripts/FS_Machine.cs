using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FS_Machine : MonoBehaviour
{

    public NavMeshAgent agent;
    public NavMeshAgent police;
    public GameObject key;

    private float _stealDist = 1;

    [Header("Wander Settings")]

    [Range(0, 100)] public float WanderSpeed = 1f;
    [Range(1, 500)] public float walkRadius = 10f;
    [Range(0, 100)] public int maxRestingTime = 5;
    int restingTime = 1;

    [Header("Approach Settings")]

    //[Range(0, 100)] public float f
    [Range(0, 100)] public float SafeDistance;
    [Range(0, 10)] public float ApproachSpeed = 5f;

    [Header("Hide Settings")]



    [Header("Coroutine Settings")]

    //public bool enabled = true;
    private WaitForSeconds wait = new WaitForSeconds(0.05f);
    delegate IEnumerator State();
    private State state;

    IEnumerator Start()
    {
        state = Wander;

        while (enabled)
        {
            yield return StartCoroutine(state());
        }
    }

    IEnumerator Wander()
    {
        Debug.Log("Wander state");
        agent.speed = WanderSpeed;

        while (Vector3.Distance(police.transform.position, key.transform.position) < SafeDistance)
        {
            WanderF();

            yield return wait;
        };

        state = Approach;
    }

    IEnumerator Approach()
    {
        Debug.Log("Approach state");

        agent.speed = ApproachSpeed;
        Seek(key.transform.position);

        bool stolen = false;
        while (Vector3.Distance(police.transform.position, key.transform.position) > SafeDistance)
        {
            if (Vector3.Distance(key.transform.position, transform.position) < ApproachSpeed)
            {
                stolen = true;
                break;
            };
            yield return wait;
        };

        if (stolen)
        {
            Steal();
            state = Hide;
        }
        else
        {
            agent.speed = 1f;
            state = Wander;
        }
    }

    IEnumerator Hide()
    {
        while (true)
        {


            yield return wait;
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame

    void Seek(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    void Steal()
    {
        key.GetComponent<Renderer>().enabled = false;
        Debug.Log("Stolen");
    }

    void WanderF()
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
