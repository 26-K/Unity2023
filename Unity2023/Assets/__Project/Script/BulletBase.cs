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
    public void AddBounce(float rate)
    {
        float maxAngle = 5.0f;
        // ランダムな角度を計算
        float randomAngle = Random.Range(-maxAngle, maxAngle) + 90;

        // 弾き飛ばす方向を計算
        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * this.transform.rotation.eulerAngles;

        // 弾き飛ばす力を計算
        Vector2 force = direction.normalized * rgd.velocity.magnitude * 10.0f;
        rgd.AddForce(force,ForceMode.Impulse);
        //rgd.velocity = rgd.velocity * -3;
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
