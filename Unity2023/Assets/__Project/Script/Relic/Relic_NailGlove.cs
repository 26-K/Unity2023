using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_NailGlove : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void BattleStartInit()
    {
    }

    public override void ObjectHitCheck(string objectName)
    {
        if (objectName == "Nail")
        {
            relicParam.count++;
            if (relicParam.count >= 2)
            {
                relicParam.count = 0;
                DoObtainEffect();
            }
        }
    }

    public override void DoEffect()
    {
        InGameManager.Ins.GetEnemyManager().AddDamage(1);
    }
}
