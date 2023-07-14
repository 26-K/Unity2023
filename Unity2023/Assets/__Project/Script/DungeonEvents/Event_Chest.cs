using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Chest : EventBase
{
    public override void Select_1()
    {
        InGameManager.Ins.GetBattleRewardManager().ShowAndLotteryRewardCards(3, Rarity.SuperRare);
        InGameManager.Ins.GetPlayerInfoManager().floor--;
    }
    public override void Select_2()
    {
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
    }
}
