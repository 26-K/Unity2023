using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EnemyActionType
{
    Attack, //通常攻撃
    Defence, //ディフェンス
    Quote, //セリフ
    End, //終了
}

[Serializable]
public class EnemyAction
{
    public EnemyActionType actionType = EnemyActionType.Attack;
    [ShowIf("@(actionType != EnemyActionType.End && actionType != EnemyActionType.Quote)")]
    public int actionPow = 1;
    [ShowIf("@actionType == EnemyActionType.Quote")]
    [LabelText("セリフ")] public string quote = "";
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int BaseHP = 10;
    int currentHP = 1;
    [SerializeField] Animator anim;
    public List<EnemyAction> enemyActions = new List<EnemyAction>();
    int currentAction = 0;
    bool alreadyAction = false;

    public void Init()
    {
        alreadyAction = false;
        currentAction = 0;
        currentHP = BaseHP;
    }

    public void TurnProgression()
    {
        currentAction++;
        alreadyAction = false;
    }

    public void ObtainDamage(int damageVal)
    {
        currentHP -= damageVal;
        if (currentHP <= 0)
        {
            anim.Play("Dead");
        }
        else
        {
            anim.Play("Damage");
        }
    }
    public List<EnemyAction> GetEnemyActions()
    {
        int actionCnt = 0;
        List<EnemyAction> retAction = new List<EnemyAction>();
        bool addMode = false;
        foreach (var a in enemyActions) //敵の行動パターンの取得
        {
            if (currentAction == actionCnt) //現在のターンと一致する区切りの場合Returnする行動パターンに追加
            {
                addMode = true;
                retAction.Add(a);
            }
            if (a.actionType == EnemyActionType.End) //end区切りで取得
            {
                if (addMode == true) //1つでも取得していた場合終わり
                {
                    break;
                }
                actionCnt++;
            }
        }

        if (retAction.Count == 0) //敵の行動パターンが見つからなかった場合アクションIDを0に戻して最初から
        {
            currentAction = 0;
            GetEnemyActions();
        }
        return retAction;
    }

    public void DoEnemyAction()
    {
        alreadyAction = true;
        foreach (var a in GetEnemyActions())
        {
            if (a.actionType == EnemyActionType.Attack)
            {

            }
            if (a.actionType == EnemyActionType.Defence)
            {

            }
            if (a.actionType == EnemyActionType.Quote)
            {
                InGameManager.Ins.GetEnemyManager().DoQuote(this, a.quote);
            }
        }
    }

    public bool GetIsActionEnd()
    {
        return alreadyAction;
    }
}
