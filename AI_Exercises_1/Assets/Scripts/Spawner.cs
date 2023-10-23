using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numAgents = 5;
    public Vector2 Range = new Vector2(5, 5);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numAgents; i++)
        {
            Vector3 randomPosition = this.transform.position + new Vector3(Random.Range(-Range.x, Range.x), 0, Random.Range(-Range.y, Range.y));
            float randomYRotation = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0f, randomYRotation, 0f);
            GameObject _agent = Instantiate(agentPrefab, randomPosition, randomRotation);
            _agent.transform.parent = transform;
        }
    }
}
