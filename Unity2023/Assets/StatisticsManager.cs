using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : SingletonMonoBehaviour<StatisticsManager>
{
    float playTime = 0.0f;
    int useCount = 0;
    int totalUseCountScore = 0;
    int diffTurnScore = 0;
    public int GetTotalUseCountScore() => totalUseCountScore;
    public int GetTotalDiffTurnScore() => diffTurnScore * 3;
    protected override void UnityAwake()
    {
        playTime = 0.0f;
    }

    private void Update()
    {
        playTime += Time.deltaTime;
    }
    public void BattleStart()
    {
        useCount = 0;
    }
    public void AddUseCount() => useCount++;
    public void BattleEnd()
    {
        int turn = TurnManager.Ins.GetElapsedTurn;
        diffTurnScore += (10 - turn);
        if (useCount <= 10)
        {
            totalUseCountScore += 10;
        }
        else if(useCount <= 20)
        {
            totalUseCountScore += 5;
        }
        else
        {
            totalUseCountScore += (20 - useCount);
        }
        useCount = 0;
    }
    public string GetTimeString(float time)
    {
        int sec = (int)time;
        int mm = sec / 60;
        int ss = sec % 60;
        int ms = (int)(time * 100.0f) % 100;
        return mm.ToString("D2") + "'" + ss.ToString("D2") + "\"" + ms.ToString("D2");
    }
}
