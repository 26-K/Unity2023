using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject baseField;
    [SerializeField] List<SetObjectBase> setObjects = new List<SetObjectBase>();
    [SerializeField] GameObject objectParent;
    [SerializeField] BulletBase pfBullet;
    [SerializeField] List<BulletBase> bulletBases;
    [SerializeField] GameObject pfLaunchArrow;
    [SerializeField] List<GameObject> launchArrows;
    [SerializeField] GameObject removeZone;
    [SerializeField] Transform leftLaunchPos;
    [SerializeField] Transform rightLaunchPos;
    [SerializeField] [LabelText("フィールドのチェックインターバル")] float fieldCheckInterval = 0.5f;
    float timer = 0.0f;
    float launchTimer = 0.0f; //発射されてからの時間、一定時間立つと強制次のターン(ハマリ防止)
    // Start is called before the first frame update
    void Start()
    {
        ResetField();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (TurnManager.Ins.GetCurrentTurn == TurnState.PlayerTurn_Wait)
        {
            launchTimer += Time.deltaTime;
            if (fieldCheckInterval <= timer) //一定時間ごとにターン終了チェック
            {
                timer = 0.0f;
                RefreshBullets();
                CheckTurnEnd();
            }
        }
    }

    /// <summary>
    /// ターン開始に宣言される発射場所
    /// </summary>
    public void LaunchReady()
    {
        timer = 0.0f;
        int launchCount = 2;
        for (int i = 0; i < launchCount; i++)
        {
            AddLaunchPos();
        }
    }

    public void AddLaunchPos()
    {
        Vector3 launchPos = new Vector3(leftLaunchPos.position.x, leftLaunchPos.position.y);
        launchPos.x = Random.Range(leftLaunchPos.position.x, rightLaunchPos.position.x);
        var a = Instantiate(pfLaunchArrow, objectParent.transform);
        a.transform.position = launchPos;
        launchArrows.Add(a);
    }

    public void LaunchStart()
    {
        timer = 0.0f;
        launchTimer = 0.0f;
        foreach (var a in launchArrows)
        {
            Vector3 launchPos = a.transform.position;
            launchPos.z = 0;
            var b = Instantiate(pfBullet, baseField.transform);
            b.Launch();
            b.transform.position = launchPos;
            b.transform.localPosition = new Vector3(b.transform.localPosition.x, b.transform.localPosition.y, -1);
            b.AddRigid(Vector3.down);
            bulletBases.Add(b);
            Destroy(a.gameObject);
        }
        launchArrows.Clear();
    }

    public void AddLaunch(Vector3 pos)
    {
        Vector3 launchPos = pos + Vector3.up * 2;
        launchPos.z = 0;
        var b = Instantiate(pfBullet, baseField.transform);
        b.Launch();
        b.transform.position = launchPos;
        b.transform.localPosition = new Vector3(b.transform.localPosition.x, b.transform.localPosition.y, -1);
        b.AddRigid(Vector3.down + (Random.Range(1, 2) == 1 ? (Vector3.left) : Vector3.right));
        bulletBases.Add(b);
    }

    public void ResetField()
    {
        foreach (var a in setObjects)
        {
            Destroy(a.gameObject);
        }
        setObjects.Clear();
    }

    public void RefreshBullets()
    {
        // リスト内のオブジェクトを条件で検査し、Destroyと除外を行います
        for (int i = bulletBases.Count - 1; i >= 0; i--)
        {
            BulletBase obj = bulletBases[i];
            if (obj.transform.position.y <= removeZone.transform.position.y)
            {
                InGameManager.Ins.GetPlayerInfoManager().SetMaxCombo(obj.GetCombo());
                Destroy(obj.gameObject);
                bulletBases.RemoveAt(i);
            }
        }
    }

    public void CheckTurnEnd()
    {
        if (JudgeTurnEnd())
        {
            TurnManager.Ins.AllFinishPlayerTurn();
        }
    }

    bool JudgeTurnEnd()
    {
        float turnEndTime = 25.0f;
        return (bulletBases.Count <= 0) || launchTimer >= turnEndTime;
    }

    public void SetObject(SetObjectBase obj)
    {
        obj.transform.parent = this.objectParent.transform;
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
        Vector3 targetPos = obj.transform.localPosition;
        targetPos.z = 0;
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y + 0.1f, 0);
        obj.transform.DOLocalMove(targetPos - Vector3.up * 0.1f, 0.3f);
        setObjects.Add(obj);
    }
}
