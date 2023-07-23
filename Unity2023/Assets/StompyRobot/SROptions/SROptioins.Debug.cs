using UnityEngine;
using System;
using System.ComponentModel;
using System.Collections.Generic;


/// <summary>
/// 全般のデバッグ機能
/// </summary>
public partial class SROptions
{
    #region 定数

    /// <summary>
    /// 全般カテゴリ
    /// </summary>
    private const string GeneralCategory = "General";

    #endregion


    #region デバッグ機能

    [Category(GeneralCategory)]
    [DisplayName("TimeScale")]
    [Sort(0)]
    [Increment(0.1)]
    [NumberRange(0.0, 10.0)]
    public float TimeScale
    {
        get { return Time.timeScale; }
        set { Time.timeScale = value; }
    }

    [Category(GeneralCategory)]
    [DisplayName("AddRelic")]
    [Sort(1)]
    public void DisplayDateTime()
    {
#if UNITY_EDITOR
        List<int> alreadyId = new List<int>();
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            alreadyId.Add(a.id);
        }
        var tg = InGameManager.Ins.GetDatabase().GetRandomRelic(alreadyId);
        if (tg == null)
        {
            return;
        }
        InGameManager.Ins.GetPlayerInfoManager().GetRelic(tg);
#endif
    }

    [Category(GeneralCategory)]
    [DisplayName("LightEnabled")]
    [Sort(2)]
    public bool LightEnabled
    {
        get { return GameObject.FindObjectOfType<Light>().enabled; }
        set { GameObject.FindObjectOfType<Light>().enabled = value; }
    }

    #endregion
}