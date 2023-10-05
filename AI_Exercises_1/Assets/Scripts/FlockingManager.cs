using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    //float freq = 0f;
    public GameObject beePrefab;
    public int numBee;
    public GameObject[] allBees;
    private Vector3 randPosBee;

    [Range(0, 100)] public float posBee_x = 0f;
    [Range(0, 100)] public float posBee_y = 0f;
    [Range(0, 100)] public float posBee_z = 0f;

    [Range(0, 10)] public float Max_Speed = 5f;
    [Range(0, 10)] public float Min_Speed = 5f;
    [Range(0, 10)] public float Neighbour_Dist = 5f;
    [Range(0, 10)] public float Rotation_Speed = 1f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        allBees = new GameObject[numBee];
        for (int i = 0; i < numBee; ++i)
        {
            Vector3 pos = this.transform.position + randPosBee;
            Vector3 randomize = randPosBee;
            allBees[i] = (GameObject)Instantiate(beePrefab, pos, Quaternion.LookRotation(randomize));
            //allBees[i].GetComponent<Flock>().myManager = this;
        }
    }
}
