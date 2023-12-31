﻿using Sirenix.OdinInspector;
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
    public int GetActionPow(int turn)
    {
        if (actionType == EnemyActionType.Attack)
        {
            int val = AssensionManager.Ins.GetAddTurnAtk() * Math.Max(turn - 2, 0);
            return (int)(actionPow * AssensionManager.Ins.GetAddAtkRate() + val);
        }
        return actionPow;
    }
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int BaseHP = 10;
    int currentHP = 1;
    int shield = 0;
    [SerializeField] Animator anim;
    public List<EnemyAction> enemyActions = new List<EnemyAction>();
    int currentAction = 0;
    bool alreadyAction = false;

    public void Init()
    {
        alreadyAction = false;
        currentAction = 1;
        BaseHP = (int)(BaseHP * AssensionManager.Ins.GetAddHpRate());
        currentHP = BaseHP;
        shield = 0;
        currentAction += AssensionManager.Ins.GetSkipEnemyAction();
    }

    public int GetBaseHP() => BaseHP;
    public int GetNowHP() => currentHP;
    public int GetShield() => shield;
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
        int actionCnt = 1;
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
            currentAction = 1;
            actionCnt = 1;

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

        }
        return retAction;
    }

    /// <summary>
    /// 敵の行動
    /// </summary>
    public void DoEnemyAction()
    {
        if (GetNowHP() <= 0) //倒されている場合は行動しない
        {
            alreadyAction = true;
            return;
        }
        shield = 0;
        InGameManager.Ins.GetEnemyManager().Refresh();
        alreadyAction = true;
        foreach (var a in GetEnemyActions())
        {
            if (a.actionType == EnemyActionType.Attack)
            {
                anim.Play("Attack");
                InGameManager.Ins.GetPlayerInfoManager().AddDamage(a.GetActionPow(TurnManager.Ins.GetElapsedTurn));
            }
            if (a.actionType == EnemyActionType.Defence)
            {
                anim.Play("Attack"); //時間が無いから防御アクションでも攻撃アニメで
                shield += a.GetActionPow(TurnManager.Ins.GetElapsedTurn);
                InGameManager.Ins.GetEnemyManager().Refresh();
            }
            if (a.actionType == EnemyActionType.Quote) //喋るだけ
            {
                InGameManager.Ins.GetEnemyManager().DoQuote(this, a.quote);
            }
            Debug.Log($"{a.actionType}");
        }
    }

    public bool GetIsActionEnd()
    {
        return alreadyAction;
    }
}
