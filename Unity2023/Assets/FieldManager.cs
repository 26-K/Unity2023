using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
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
            if (fieldCheckInterval >= timer) //一定時間ごとにターン終了チェック
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
        int launchCount = 1;
        for (int i = 0; i < launchCount; i++)
        {
            Vector3 launchPos = new Vector3(leftLaunchPos.position.x, leftLaunchPos.position.y);
            launchPos.x = Random.Range(leftLaunchPos.position.x, rightLaunchPos.position.x);
            var a = Instantiate(pfLaunchArrow, objectParent.transform);
            a.transform.position = launchPos;
        }
    }

    public void LaunchStart()
    {
        timer = 0.0f;
        launchTimer = 0.0f;
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
                Destroy(obj);
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
        setObjects.Add(obj);
    }
}
