using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_StartDash : RelicBase
{
    /// <summary>
    /// バトル開始時
    /// </summary>
    public override void TurnCheck(int turn)
    {
        if (turn == 1)
        {
            DoObtainEffect();
        }
    }

    public override void DoEffect()
    {
        InGameManager.Ins.GetCardManager().ChangeMana(1);
        InGameManager.Ins.GetCardManager().DrawCard();
    }
}
