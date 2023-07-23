using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_HappyFlower : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void TurnCheck(int turn)
    {
        relicParam.count++;
        if (relicParam.count == 3)
        {
            DoObtainEffect();
        }
    }

    public override void DoEffect()
    {
        AudioManager.Ins.PlayDuplicateSound();
        relicParam.count = 0;
        InGameManager.Ins.GetFieldManager().AddLaunchPos();
    }
}
