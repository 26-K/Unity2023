using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    void HitObject();
}

public enum ObjectType
{
    Normal,
    Gate,
}

public class SetObjectBase : MonoBehaviour
{
    int ignoreFlame = 0;
    [SerializeField] List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    [SerializeField] ObjectType objectType = ObjectType.Normal;
    public ObjectType GetObjectType() => objectType;
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
        SetLayerRecursively(this.transform.gameObject, 2);
        this.transform.gameObject.SetLayerRecursively(2);
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

    public void SetLayerRecursively(GameObject self,int layer)
    {
        self.layer = layer;

        foreach (Transform n in self.transform)
        {
            SetLayerRecursively(n.gameObject, layer);
        }
    }
}

public static class GameObjectExtensions
{
    /// <summary>
    /// 自分自身を含むすべての子オブジェクトのレイヤーを設定します
    /// </summary>
    public static void SetLayerRecursively(
        this GameObject self,
        int layer
    )
    {
        self.layer = layer;

        foreach (Transform n in self.transform)
        {
            SetLayerRecursively(n.gameObject, layer);
        }
    }
}