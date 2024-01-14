using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{
    public override bool Check()
    {
        GameObject cop = GameObject.Find("Knight_Guard_BB");
        GameObject treasure = GameObject.Find("Key");
        return Vector3.Distance(cop.transform.position, treasure.transform.position) < 5f;
    }
}
