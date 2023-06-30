using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectBase : MonoBehaviour
{
    int ignoreFlame = 0;
    [SerializeField] List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private void Update()
    {
        ignoreFlame--;
        SelfUpdate();
    }

    public virtual void SelfStart()
    {

    }

    public virtual void SelfUpdate() { }
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

    public void SetPreview()
    {
        this.transform.gameObject.layer = 2;
        foreach (var a in meshRenderers)
        {
            Color col = new Color();
            if (a.material == null)
            {
                continue;
            }
            col = a.material.color;
            col.a = 0.4f;
            a.material.color = col;
        }
    }
}
