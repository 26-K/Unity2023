using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public List<UI_RelicIcon> relicIcons = new List<UI_RelicIcon>();
    [SerializeField] GameObject instParent;
    [SerializeField] UI_RelicIcon pfRelicIcon;

    public void Init()
    {
        foreach (var a in relicIcons)
        {
            Destroy(a.gameObject);
        }
        relicIcons.Clear();
    }

    public void AddNewRelic(RelicBase relic)
    {
        var a = Instantiate(pfRelicIcon, instParent.transform);
        a.Init(relic);
        relicIcons.Add(a);
        RefreshCount();
    }

    public void RefreshCount()
    {
        foreach (var a in relicIcons)
        {
            a.RefreshCount();
        }
    }

    public void ComboEnableCheck(int currentCombo)
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.ComboEnableCheck(currentCombo);
        }
        RefreshCount();
    }
    public void BattleStartCheck()
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.BattleStartInit();
        }
        RefreshCount();
    }

    public void TurnStartCheck(int turn)
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.TurnCheck(turn);
        }
        RefreshCount();
    }

    public void ObjectHitCheck(string ObjectName)
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.ObjectHitCheck(ObjectName);
        }
        RefreshCount();
    }
    public void AddDamage(int val)
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.AddDamage(val);
        }
        RefreshCount();
    }
    public void WallHitEnableCheck()
    {
        foreach (var a in InGameManager.Ins.GetPlayerInfoManager().relics)
        {
            a.WallHitCheck();
        }
        RefreshCount();
    }
}
