using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_SpinNail : SetObjectBase,IObject
{
    [SerializeField] BoxCollider col;
    float timer = 0.0f;
    string objectName = "Nail";
    public override void SelfUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            col.isTrigger = true;
        }
        Vector3 speed = new Vector3(0, 0, 30);
        this.transform.localRotation *= Quaternion.Euler(speed * Time.deltaTime);
    }

    public override void SelfStart()
    {
        timer = 0.0f;
        col.isTrigger = false;
    }

    public void HitObject()
    {
        InGameManager.Ins.GetRelicManager().ObjectHitCheck(objectName);
    }
}
