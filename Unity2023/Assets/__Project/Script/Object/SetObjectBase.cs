using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectBase : MonoBehaviour
{
    int ignoreFlame = 0;
    private void Update()
    {
        ignoreFlame--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && ignoreFlame <= 0)
        {
            TriggerEnterAction(other);
            ignoreFlame = 2;
        }
    }

    public virtual void TriggerEnterAction(Collider collider) { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            TouchEnterAction(collision);
        }
    }

    public virtual void TouchEnterAction(Collision collision) { }
}
