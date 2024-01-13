using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek_NavMesh : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent target;

    float freq = 0f;

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            AI_Scripts.Mov_Scripts.SeekTarget(agent, target.transform);
        }
    }
}