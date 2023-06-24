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
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] Animator anim;
    public List<EnemyAction> enemyActions = new List<EnemyAction>();
    int currentAction = 0;
    void Start()
    {
        currentAction = 0;
    }

    public void TurnProgression()
    {
        currentAction++;
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
}
