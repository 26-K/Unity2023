using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    protected override void UnityAwake()
    {
    }


    /// <summary>
    /// この書き方はよくない
    /// </summary>
    /// <returns></returns>
    public int GetNowScore()
    {
        int totalScore = 0;
        int add = InGameManager.Ins.GetPlayerInfoManager().floor * 30;
        totalScore += add;

        if (InGameManager.Ins.GetPlayerInfoManager().floor >= 13 && InGameManager.Ins.GetPlayerInfoManager().hp >= 1)
        {
            totalScore += 500;
        }

        add = InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses.Count * 15;
        totalScore += add;

        add = Mathf.Max(InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo, 20) * 15;
        totalScore += add;

        add = (int)(InGameManager.Ins.GetPlayerInfoManager().GetDiffDamage * 0.6f);
        totalScore += add;

        add = (int)(InGameManager.Ins.GetPlayerInfoManager().relics.Count * 50.0f);
        totalScore += add;

        return totalScore;
    }
}
