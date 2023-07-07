using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RemoveCardManager : SingletonMonoBehaviour<UI_RemoveCardManager>
{
    [SerializeField] List<BattleCardBase> battleCards = new List<BattleCardBase>();
    protected override void UnityAwake()
    {
    }

    public void InitShow()
    {
        var cards = InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses;
        foreach (var a in cards)
        {

        }
    }
}
