using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public int hp = 50;
    public int maxHp = 50;
    InGameManager parent;
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
        foreach (var a in parent.GetDatabase().firstCards)
        {
            BattleCardStatus st = new BattleCardStatus();
            st.id = a.id;
            battleCardStatuses.Add(st);
        }
    }
}
