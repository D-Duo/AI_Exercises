using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Manager : MonoBehaviour
{
    public static Zombie_Manager myManager;
    public NavMeshAgent zombiePrefab;
    public int numZombies = 10;
    public NavMeshAgent[] allZombies;
    public Vector3 spawn = new Vector3(5, 1, 5);
    Vector3 randPos;

    // Start is called before the first frame update
    void Start()
    {
        allZombies = new NavMeshAgent[numZombies];
        for (int i = 0; i < numZombies; ++i)
        {
            randPos = this.transform.position + new Vector3(Random.Range(-spawn.x, spawn.x),
                                                                Random.Range(-spawn.y, spawn.y),
                                                                Random.Range(-spawn.z, spawn.z));
            //Vector3 randomize = randPosBee;
            allZombies[i] = (NavMeshAgent)Instantiate(zombiePrefab, randPos, Quaternion.identity);
            
        }
        myManager = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PublicMessage(NavMeshAgent target)
    {
        gameObject.BroadcastMessage("follow target", target);
    }
}
