using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    CharacterBase owner;
    public Vector3 spd;
    public Rigidbody rgd;

    float sleepTime = 0.0f;

    public int pow = 1;
    int ignoreFlame = 7;
    int hitcount = 0;
    int totalHitcount = 0;
    public virtual void Launch(CharacterBase character)
    {
        ignoreFlame = 7;
        sleepTime = 0.0f;
        hitcount = 0;
        totalHitcount = 0;
    }
    public void Launch(int pow = 5)
    {
        ignoreFlame = 7;
        sleepTime = 0.0f;
        hitcount = 0;
        this.pow = pow;
    }

    public int CalcPow()
    {
        return hitcount + pow;
    }

    public int GetCombo()
    {
        return hitcount;
    }

    public void Update()
    {
        ignoreFlame--;
        if (rgd.IsSleeping())
        {
            sleepTime += Time.deltaTime;
            if (sleepTime >= 1.3f)
            {
                this.transform.position = Vector3.down * 100;
            }
        }
        
    }

    public void ResetCombo()
    {
        hitcount = 0;
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
        Vector2 force = direction.normalized * rgd.velocity.magnitude * 10000.0f;
        rgd.AddForce(force,ForceMode.Impulse);
        //rgd.velocity = rgd.velocity * -3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitcount++;
        totalHitcount++;
        if (hitcount % 5 == 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collision.transform, $"Combo{hitcount}\nPow+{hitcount}");
        }
        Debug.Log($"combo{hitcount}");
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

    public bool IsRemoveReady()
    {
        return false;
    }
}
