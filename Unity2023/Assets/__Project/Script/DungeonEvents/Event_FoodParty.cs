using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FoodParty : EventBase
{
    public override void Select_1()
    {
        InGameManager.Ins.GetPlayerInfoManager().maxHp += 5;
        InGameManager.Ins.GetPlayerInfoManager().AddHeal(5);
    }
    public override void Select_2()
    {
        InGameManager.Ins.GetPlayerInfoManager().AddHp(20);
    }
    public override void Select_3()
    {
        UI_RemoveCardManager.Ins.InitShow();
    }
}
