using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase ,IDamageable
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float baseMoveSpd;
    [SerializeField] BulletBase shotBullet;
    [SerializeField] GameObject bulletShootPos;
    public void Init()
    {
        side = SideType.Side_Player;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var obj = Instantiate(shotBullet);
            obj.transform.position = bulletShootPos.transform.position;
            obj.Launch(this);

            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mousePos = Input.mousePosition;

            mousePos.z = 20f;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            float angle = GetAngle(this.transform.position, mouseWorldPos);
            Debug.Log(angle);
            // 角度をラジアンに変換
            float rad = angle * Mathf.Deg2Rad;
            // ラジアンから進行方向を設定
            Vector3 direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            //// 向きの生成（Z成分の除去と正規化）
            //Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;


            obj.AddRigid(direction);
        }
    }

    public void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(Vector3.left * baseMoveSpd);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(Vector3.right * baseMoveSpd);
        }
    }
    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    public void TakeDamage(int damage,CharacterBase owner)
    {
        if (owner != this)
        {
            return;
        }
        this.HP -= damage;
    }
}
