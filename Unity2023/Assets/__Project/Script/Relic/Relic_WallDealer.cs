using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_WallDealer : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void BattleStartInit()
    {
    }

    public override void WallHitCheck()
    {
        relicParam.count++;
        if (relicParam.count >= 7)
        {
            relicParam.count = 0;
            DoObtainEffect();
        }
        
    }

    public override void DoEffect()
    {
        AudioManager.Ins.PlayDuplicateSound();
        InGameManager.Ins.GetCardManager().DrawCard();
    }
}
