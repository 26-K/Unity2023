using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Bumper : SetObjectBase,IObject
{
    [SerializeField]Animator anim;

    public void HitObject()
    {
        
    }

    public override void TouchEnterAction(Collision collision)
    {
        anim.Play("default");
        collision.gameObject.GetComponent<BulletBase>().AddBounce(2.8f);
    }
}
