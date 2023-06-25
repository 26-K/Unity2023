﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI scoreText;

    public void Init()
    {
        text.text = "";
        scoreText.text = "";
        int totalScore = 0;
        int add = InGameManager.Ins.GetPlayerInfoManager().floor * 30;
        totalScore += add;
        text.text += $"\nフロア進行 -----";
        scoreText.text += $"\n{add}";

        add = InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses.Count * 20;
        totalScore += add;
        text.text += $"\nカード所持数 -----";
        scoreText.text += $"\n{add}";

        add = InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo * 12;
        totalScore += add;
        text.text += $"\n最大コンボ -----";
        scoreText.text += $"\n{add}";

        add = InGameManager.Ins.GetPlayerInfoManager().GetDiffDamage * 8;
        totalScore += add;
        text.text += $"\n与被ダメージ差分 -----";
        scoreText.text += $"\n{add}";

        text.text += $"\n---------------------";
        scoreText.text += $"\n-------";

        text.text += $"\nTotal";
        scoreText.text += $"\n{totalScore}";
    }
}