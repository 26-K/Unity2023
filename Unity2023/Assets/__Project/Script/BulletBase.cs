using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public CharacterBase owner;
    public Vector3 spd;
    public Rigidbody rgd;

    public int pow = 1;
    int ignoreFlame = 7;

    public virtual void Launch(CharacterBase character)
    {
        ignoreFlame = 7;
    }

    public void Update()
    {
        ignoreFlame--;
    }

    public void AddRigid(Vector3 vec)
    {
        rgd.velocity = vec * 50;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreFlame > 0)
        {
            Debug.Log("ignore");
            return;
        }
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log("hit");
            ignoreFlame = 2;
            damageable.TakeDamage(pow, owner);
        }
    }
}
