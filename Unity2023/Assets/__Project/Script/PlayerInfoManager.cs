using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    InGameManager parent;
    public List<BattleCardStatus> battleCardStatuses = new List<BattleCardStatus>();

    public void Init(InGameManager inGameManager)
    {
        this.parent = inGameManager;
        battleCardStatuses.Clear();
    }
}
