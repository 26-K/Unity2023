using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

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
        text.text += $"\nフロア進行 ";
        scoreText.text += $"\n{add}";
        if (InGameManager.Ins.GetPlayerInfoManager().floor >= 13 && InGameManager.Ins.GetPlayerInfoManager().hp >= 1)
        {
            totalScore += 300;
            text.text += $"\nクリアおめでとう!";
            scoreText.text += $"\n{totalScore}";
        }

        add = InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses.Count * 15;
        totalScore += add;
        text.text += $"\nカード所持数 ";
        scoreText.text += $"\n{add}";

        add = InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo * 12;
        totalScore += add;
        text.text += $"\n最大コンボ ";
        scoreText.text += $"\n{add}";

        add = (int)(InGameManager.Ins.GetPlayerInfoManager().GetDiffDamage * 0.4f);
        totalScore += add;
        text.text += $"\n与被ダメージ差分 ";
        scoreText.text += $"\n{add}";

        text.text += $"\n---------------------";
        scoreText.text += $"\n---------------------";

        text.text += $"\nTotal";
        scoreText.text += $"\n{totalScore}";

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(totalScore);
        UnityroomApiClient.Instance.SendScore(1, totalScore, ScoreboardWriteMode.Always);
        UnityroomApiClient.Instance.SendScore(2, InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo, ScoreboardWriteMode.Always);
    }
}
