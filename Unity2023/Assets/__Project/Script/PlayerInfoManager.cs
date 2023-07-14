using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<RelicBase> relics = new List<RelicBase>();
    int maxCombo = 0;
    int diffDamage = 0;
    int overkillDamage = 0;
    public void Init(InGameManager inGameManager)
    {
        this.parent = inGameManager;
        maxCombo = 0;
        diffDamage = 0;
        overkillDamage = 0;
    }

    public void GameStartInit()
    {
        battleCardStatuses.Clear();
        hp = 50;
        maxHp = 50;
        floor = 1;
        relics.Clear(); //レリック初期化
        foreach (var a in parent.GetDatabase().firstCards)
        {
            BattleCardStatus st = new BattleCardStatus();
            st.id = a.id;
            AddCardInDeck(st);
        }
        hpGauge.Refresh(hp, maxHp, guard);
    }

    public void AddCardInDeck(BattleCardStatus bt)
    {
        int maxIndex = 0;
        if (battleCardStatuses.Count > 0)
        {
            maxIndex = battleCardStatuses.Max(x => x.index);
        }
        bt.index = maxIndex + 1;
        battleCardStatuses.Add(bt);
    }
    public void RemoveCardInDeck(BattleCardStatus bt)
    {
        battleCardStatuses.RemoveAll(x => x.index == bt.index);
    }

    public void GetRelic(RelicBase relic)
    {
        relic.InitObtain();
        relics.Add(relic);
        InGameManager.Ins.GetRelicManager().AddNewRelic(relic);
    }

    public void SetMaxCombo(int val)
    {
        maxCombo = Mathf.Max(val, maxCombo);
    }

    public void SetDiffDamage(int changeVal)
    {
        diffDamage += changeVal;
    }
    public int GetDiffDamage => diffDamage;
    public int GetMaxCombo => maxCombo;

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
            if (guard > 0)
            {
                InGameManager.Ins.GetUI_PopUpManager().ShowPopUpTextSub(player.transform, $"\nブロック！[{guard}]", "DamagePopUp");
            }
            guard = 0;
            player.PlayDamage();
        }
        else //ガード
        {
            AudioManager.Ins.PlayNailHitSound();
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpTextSub(player.transform, $"\nブロック！[{totalDamage}]", "DamagePopUp");
            guard -= totalDamage;
            totalDamage = 0;
        }
        hp -= totalDamage;
        SetDiffDamage(-totalDamage);
        if (totalDamage != 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpTextSub(player.transform, $"{totalDamage}", "DamagePopUp");
        }
        hpGauge.Refresh(hp, maxHp, guard);
        if (hp <= 0)
        {
            player.PlayDie();
            InGameManager.Ins.GetGameOverUI().ShowGameOverUI();
            InGameManager.Ins.isEndGame = true;
        }
    }

    public int AddRatioHeal(float rate)
    {
        int retVal = (int)((float)maxHp * rate);
        AddHeal(retVal);
        return retVal;
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
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        hpGauge.Refresh(hp, maxHp, guard);
    }
}
