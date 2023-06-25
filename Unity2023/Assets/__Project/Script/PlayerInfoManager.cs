using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public int hp = 50;
    public int maxHp = 50;
    public int floor = 1;
    public int guard = 0;
    InGameManager parent;
    public PlayerManager player;
    public UI_StatusGauge hpGauge;
    public List<BattleCardStatus> battleCardStatuses = new List<BattleCardStatus>();

    public void Init(InGameManager inGameManager)
    {
        this.parent = inGameManager;
    }

    public void GameStartInit()
    {
        battleCardStatuses.Clear();
        hp = 50;
        maxHp = 50;
        floor = 1;
        foreach (var a in parent.GetDatabase().firstCards)
        {
            BattleCardStatus st = new BattleCardStatus();
            st.id = a.id;
            battleCardStatuses.Add(st);
        }
        hpGauge.Refresh(hp, maxHp, guard);
    }

    public void TurnProgression()
    {
        guard = 0;
        hpGauge.Refresh(hp, maxHp, guard);
    }
    public void AddDamage(int val)
    {
        int totalDamage = val;
        if (totalDamage > guard) //防ぎきれない
        {
            totalDamage -= guard;
            guard = 0;
            player.PlayDamage();
        }
        else //ガード
        {
            totalDamage = 0;
            guard -= totalDamage;
        }
        hp -= totalDamage;
        InGameManager.Ins.GetUI_PopUpManager().ShowPopUpTextSub(player.transform, $"{totalDamage}", "DamagePopUp");
        hpGauge.Refresh(hp, maxHp, guard);
    }

    public void AddHeal(int val)
    {
        AddHp(val);
    }
    public void AddShield(int val)
    {
        guard += val;
        hpGauge.Refresh(hp, maxHp, guard);
    }

    public void AddHp(int val)
    {
        hp += val;
        if (hp > val)
        {
            hp = val;
        }
        hpGauge.Refresh(hp, maxHp, guard);
    }
}
