using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicParam
{
    public int id = 0;
    public int count = 0;
    public bool isPlayEnableAnim = false;
}

public abstract class RelicBase : ScriptableObject
{
    [LabelText("ID")]public int id = 0;
    [LabelText("レリック名")]public string relicName = "";
    [Multiline(4)][LabelText("説明")]public string relicDesc = "";
    [LabelText("カウントレリックか")] public bool countRelic = false;
    [PreviewField(100)] [LabelText("画像")] public Sprite imageSprite;
    [HideInInspector] public RelicParam relicParam = new RelicParam();
    /// <summary>
    /// 取得時の効果
    /// </summary>
    public virtual void InitObtain()
    {

    }

    /// <summary>
    /// バトル開始時
    /// </summary>
    public virtual void BattleStartInit()
    {

    }

    /// <summary>
    /// ターン開始時
    /// </summary>
    public virtual void TurnCheck(int turn)
    {

    }

    public virtual void ComboEnableCheck(int currentCombo)
    {

    }
    public virtual void WallHitCheck()
    {

    }
    public virtual void ObjectHitCheck(string objectName)
    {

    }

    public void DoObtainEffect()
    {
        relicParam.isPlayEnableAnim = true;
        DoEffect();
    }

    public abstract void DoEffect();
}
