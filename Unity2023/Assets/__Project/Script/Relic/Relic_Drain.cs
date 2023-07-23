using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Drain : RelicBase
{
    
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void BattleStartInit()
    {
    }

    public override void AddDamage(int damage)
    {
        relicParam.count += damage;
        if (relicParam.count >= 25)
        {
            DoObtainEffect();
        }
    }

    public override void DoEffect()
    {
        relicParam.count -= 25;
        InGameManager.Ins.GetPlayerInfoManager().AddHeal(1);
    }
}
