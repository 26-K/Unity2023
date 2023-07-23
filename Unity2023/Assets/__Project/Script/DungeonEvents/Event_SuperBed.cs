using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_SuperBed : EventBase
{
    public override void Select_1()
    {
        InGameManager.Ins.GetPlayerInfoManager().AddRatioHeal(1);
    }
    public override void Select_2()
    {
        UI_RemoveCardManager.Ins.InitShow();
    }
}
