using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterAction(other);
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
