using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_mov : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;

    float freq = 0f;

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            Seek();
        }
    }

    void Seek()
    {
        agent.destination = target.transform.position;
    }

}