using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_AttackGate : SetObjectBase
{
    public override void TriggerEnterAction(Collider collider)
    {
        base.TriggerEnterAction(collider);
        Debug.LogError("Todo:攻撃力アップ");
    }
}
