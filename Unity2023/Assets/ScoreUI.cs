using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI scoreText;
    int retScore = 0;
    public void Init()
    {
        text.text = "";
        scoreText.text = "";
        int totalScore = 0;
        int add = InGameManager.Ins.GetPlayerInfoManager().floor * 30;
        totalScore += add;
        text.text += $"\nフロア進行 ";
        scoreText.text += $"\n{add}";

        if (InGameManager.Ins.GetPlayerInfoManager().floor >= 20 && InGameManager.Ins.GetPlayerInfoManager().hp >= 1)
        {
            totalScore += 500;
            text.text += $"\nクリアおめでとう!";
            scoreText.text += $"\n{500}";
        }

        add = InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses.Count * 15;
        totalScore += add;
        text.text += $"\nカード所持数 ";
        scoreText.text += $"\n{add}";

        add = Mathf.Min(InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo, 20) * 15;
        totalScore += add;
        text.text += $"\n最大コンボ ";
        scoreText.text += $"\n{add}";

        add = (int)(InGameManager.Ins.GetPlayerInfoManager().GetDiffDamage * 0.6f);
        totalScore += add;
        text.text += $"\n与被ダメージ差分 ";
        scoreText.text += $"\n{add}";


        add = Mathf.Min((int)(StatisticsManager.Ins.GetTotalUseCountScore() + StatisticsManager.Ins.GetTotalDiffTurnScore()), 3000);
        totalScore += add;
        text.text += $"\nクイック撃破";
        scoreText.text += $"\n{add}";

        add = (int)(InGameManager.Ins.GetPlayerInfoManager().relics.Count * 80.0f);
        totalScore += add;
        text.text += $"\nレリック所持数 ";
        scoreText.text += $"\n{add}";

        int alv = AssensionManager.Ins.GetAssension();
        if (alv >= 1)
        {
            add = (int)(totalScore * (alv * 0.05f));
            totalScore += add;
            text.text += $"\nヘルモード";
            scoreText.text += $"\n{add}";
        }

        text.text += $"\n---------------------";
        scoreText.text += $"\n---------------------";

        text.text += $"\nTotal";
        scoreText.text += $"\n{totalScore}";

        UnityroomApiClient.Instance.SendScore(1, totalScore, ScoreboardWriteMode.Always);
        UnityroomApiClient.Instance.SendScore(2, InGameManager.Ins.GetPlayerInfoManager().GetMaxCombo, ScoreboardWriteMode.Always);

        retScore = totalScore;
    }

    public void ShowHiScore()
    {
        if (retScore >= 9999)
        {
            retScore = 9999;
        }
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(retScore);
    }

    public int GetScore()
    {
        return retScore;
    }
}
