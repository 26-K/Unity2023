using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_BigHand : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void BattleStartInit()
    {
            DoObtainEffect();
    }

    public override void DoEffect()
    {
        InGameManager.Ins.GetFieldManager().ScaleGate();
    }
}
