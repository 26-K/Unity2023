using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState
{
    public int pow = 0; // pow
    public int comboCount = 0;
    public int totalComboCount = 0; // コンボリセットを考慮しないコンボ数
}

public class BulletBase : MonoBehaviour
{
    CharacterBase owner;
    public Vector3 spd;
    public Rigidbody rgd;

    float lifeTime = 20.0f;
    float sleepTime = 0.0f;
    int ignoreFlame = 7;
    public BulletState state = new BulletState();

    public virtual void Launch(CharacterBase character)
    {
        ignoreFlame = 7;
        sleepTime = 0.0f;
        this.state = new BulletState();
        state.comboCount = 0;
        state.totalComboCount = 0;
    }
    public void Launch(BulletState state = null)
    {
        ignoreFlame = 7;
        sleepTime = 0.0f;
        this.state.comboCount = 0;
        this.state.totalComboCount = 0;
        this.state.pow = 5; //初期威力

        if (state != null)
        {
            //追加ステータス
            this.state.pow += state.pow;
            this.state.comboCount += state.comboCount;
            this.state.totalComboCount += state.totalComboCount;
        }
        lifeTime = 20.0f;
    }

    public int CalcPow()
    {
        return state.comboCount + state.pow;
    }

    public int GetCombo()
    {
        return state.comboCount;
    }

    public void Update()
    {
        ignoreFlame--;
        lifeTime -= Time.deltaTime;
        if (rgd.IsSleeping())
        {
            sleepTime += Time.deltaTime;
            if (sleepTime >= 1.3f)
            {
                this.transform.position = Vector3.down * 100;
            }
        }
        if (lifeTime <= 0.0f)
        {
            this.transform.position = Vector3.down * 100;
        }

    }

    public void ResetCombo()
    {
        state.comboCount = 0;
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
        rgd.AddForce(force, ForceMode.Impulse);
        //rgd.velocity = rgd.velocity * -3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        state.comboCount++;
        state.totalComboCount++;
        InGameManager.Ins.GetRelicManager().ComboEnableCheck(state.comboCount);
        if (state.comboCount % 5 == 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collision.transform, $"Combo{state.comboCount}\nPow+{state.comboCount}");
        }
        else if (state.comboCount % 3 == 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collision.transform, $"Combo{state.comboCount}\nPow+{state.comboCount}");
        }
        Debug.Log($"combo{state.comboCount}");
        if (ignoreFlame > 0)
        {
            Debug.Log("ignore");
            return;
        }
        var hitable = collision.gameObject.GetComponent<IObject>();
        if (hitable != null)
        {
            hitable.HitObject();
        }
    }

    public bool IsRemoveReady()
    {
        return false;
    }
}
