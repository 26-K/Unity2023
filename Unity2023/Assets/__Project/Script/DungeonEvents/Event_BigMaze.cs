using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_BigMaze : EventBase
{
    public override void Select_1()
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
        InGameManager.Ins.GetPlayerInfoManager().AddDamage(12);
    }
    public override void Select_2()
    {
        InGameManager.Ins.GetPlayerInfoManager().AddHeal(12);
    }
    public override void Select_3()
    {
    }
}
