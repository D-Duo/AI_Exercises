using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed, minS, maxS;
    Vector3 LiderPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockingManager.myManager.Min_Speed, FlockingManager.myManager.Max_Speed);
        minS = FlockingManager.myManager.Min_Speed;
        maxS = FlockingManager.myManager.Max_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Bounds hiveBox = new Bounds(FlockingManager.myManager.transform.position, FlockingManager.myManager.hiveLimits);
        if (!hiveBox.Contains(transform.position))
        {
            Vector3 direction = FlockingManager.myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  FlockingManager.myManager.Rotation_Speed * Time.deltaTime);
        }

        if (minS != FlockingManager.myManager.Min_Speed || maxS != FlockingManager.myManager.Max_Speed)
        {
            speed = Random.Range(FlockingManager.myManager.Min_Speed, FlockingManager.myManager.Max_Speed);
        }

        if (Random.Range(0, 100) < FlockingManager.myManager.freqFlock)
        {
            //ApplyRules();
            AllTogether();
        }
        
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRules()
    {
        GameObject[] hive;
        hive = FlockingManager.myManager.allBees;

        Vector3 vcenter = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in hive)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= FlockingManager.myManager.Neighbour_Dist)
                {
                    vcenter += go.transform.position;
                    groupSize++;

                    if (nDistance < FlockingManager.myManager.Separation_Dist)
                        vavoid += (this.transform.position - go.transform.position);

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
            if (groupSize > 0)
            {
                vcenter = vcenter / groupSize + (FlockingManager.myManager.goalPos - transform.position);
                speed = gSpeed / groupSize;
                if (speed > FlockingManager.myManager.Max_Speed)
                    speed = FlockingManager.myManager.Max_Speed;

                Vector3 direction = (vcenter + vavoid) - transform.position;
                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                                                          Quaternion.LookRotation(direction),
                                                          FlockingManager.myManager.Rotation_Speed * Time.deltaTime);
            }
        }
    }

    void AllTogether()
    {
        GameObject[] hive;
        hive = FlockingManager.myManager.allBees;

        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        float gSpeed = 0.01f;
        float distance;
        int num = 0;

        foreach (GameObject go in hive)
        {
            if (go != this.gameObject)
            {
                distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= FlockingManager.myManager.Neighbour_Dist)
                {
                    cohesion += go.transform.position;

                    if (distance < FlockingManager.myManager.Separation_Dist)
                    {
                        separation += (transform.position - go.transform.position);
                    }

                    gSpeed += go.GetComponent<Flock>().speed;
                    num++;
                }
            }
            if (num > 0)
            {
                //cohesion = cohesion / num + (FlockingManager.myManager.goalPos - transform.position);
                cohesion = ((cohesion / num - transform.position).normalized * speed);
                if (FlockingManager.myManager.RandGoal)
                    cohesion += (FlockingManager.myManager.goalPos - transform.position);

                speed = gSpeed / num;
                if (speed > FlockingManager.myManager.Max_Speed)
                    speed = FlockingManager.myManager.Max_Speed;


                if (this.CompareTag("Lider"))
                {
                    LiderPos = transform.position;
                    Debug.Log(LiderPos);
                }

                Vector3 direction;
                if(FlockingManager.myManager.FollowLider && !this.CompareTag("Lider"))
                {
                    direction = LiderPos - transform.position;
                }
                else
                {
                    direction = (cohesion + separation) - transform.position;
                }

                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                                                          Quaternion.LookRotation(direction),
                                                          FlockingManager.myManager.Rotation_Speed * Time.deltaTime);
            }
        }
    }
}