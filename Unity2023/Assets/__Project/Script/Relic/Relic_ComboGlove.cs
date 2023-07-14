using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_ComboGlove : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void BattleStartInit()
    {
    }

    public override void ComboEnableCheck(int currentCombo)
    {
        if (currentCombo % 3 == 0)
        {
            DoObtainEffect();
        }
    }

    public override void DoEffect()
    {
        InGameManager.Ins.GetEnemyManager().AddDamage(1);
    }
}
