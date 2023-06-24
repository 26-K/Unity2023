using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Bumper : SetObjectBase
{
    [SerializeField]Animator anim;
    public override void TouchEnterAction(Collision collision)
    {
        anim.Play("default");
        collision.gameObject.GetComponent<BulletBase>().AddBounce(2.8f);
    }
}
