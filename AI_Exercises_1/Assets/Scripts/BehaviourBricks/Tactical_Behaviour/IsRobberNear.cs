using UnityEngine;
 
using Pada1.BBCore;
using Pada1.BBCore.Framework; 

[Condition("MyConditions/Is Robber Near?")]
[Help("Checks whether Robber is near the Treasure.")]
public class IsRobberNear : ConditionBase
{
    public override bool Check()
    {
        GameObject cop = GameObject.Find("Rober");
        GameObject treasure = GameObject.Find("Key");
        return Vector3.Distance(cop.transform.position, treasure.transform.position) < 10f;
    }
} 