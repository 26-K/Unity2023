using DG.Tweening;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    PlayerTurn_Wait,
    EnemyTurn,
}

public class TurnManager : SingletonMonoBehaviour<TurnManager>
{
    TurnState currentTurn;
    public TurnState GetCurrentTurn => currentTurn; //現在のターン
    int elapsedTurn = 0;
    public int GetElapsedTurn => elapsedTurn; //経過ターン
    protected override void UnityAwake()
    {
    }


    public void BattleStart()
    {
        elapsedTurn = 0;
        StatisticsManager.Ins.BattleStart();
        InGameManager.Ins.GetCardManager().AllDiscard();
        CurrentTurnManagerUI.Ins.ShowBattleStartAnim();
        InGameManager.Ins.GetCardManager().BattleStart();

        elapsedTurn++;
        currentTurn = TurnState.PlayerTurn;
        InGameManager.Ins.GetCardManager().TurnStart();
        InGameManager.Ins.GetFieldManager().LaunchReady();
        InGameManager.Ins.GetEnemyManager().TurnProgression();
        InGameManager.Ins.GetPlayerInfoManager().TurnProgression();
        InGameManager.Ins.GetRelicManager().BattleStartCheck();
        InGameManager.Ins.GetRelicManager().TurnStartCheck(elapsedTurn);
        InGameManager.Ins.GetComboUIManager().Hide();
    }

    /// <summary>
    /// ターン終了時の処理
    /// </summary>
    public void PushTurnEndButton()
    {
        PlayerTurnEnd();
        UI_Tutorial.Ins.EndBattleTutorial();
    }

    public void PlayerTurnEnd()
    {
        if (currentTurn != TurnState.PlayerTurn)
        {
            return;
        }
        InGameManager.Ins.GetCardManager().AllDiscard();
        InGameManager.Ins.GetCardManager().DiscardCardCheck();
        InGameManager.Ins.GetFieldManager().LaunchStart();
        currentTurn = TurnState.PlayerTurn_Wait;
    }

    /// <summary>
    /// 全てのPLの行動が完了した時に戦闘終了チェック
    /// </summary>
    public void AllFinishPlayerTurn()
    {
        if (InGameManager.Ins.GetEnemyManager().IsDestroyAllEnemy())
        {
            Debug.Log("Youwin");
            InGameManager.Ins.GetPlayerInfoManager().TurnProgression();
            currentTurn = TurnState.PlayerTurn;
            InGameManager.Ins.GetComboUIManager().Hide();
            InGameManager.Ins.GetCardManager().AllDiscard();
            StatisticsManager.Ins.BattleEnd();
            if (InGameManager.Ins.GetPlayerInfoManager().floor >= 20)
            {
                InGameManager.Ins.GetGameOverUI().ShowGameClearUI();
                InGameManager.Ins.isEndGame = true;
            }
            else
            {
                InGameManager.Ins.GetBattleRewardManager().ShowAndLotteryRewardCards();
            }
            return;
        }
        currentTurn = TurnState.EnemyTurn;
        InGameManager.Ins.GetComboUIManager().Hide();
        CurrentTurnManagerUI.Ins.ShowEnemyTurnAnim();
    }

    public void FinishEnemyTurn()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        elapsedTurn++;
        currentTurn = TurnState.PlayerTurn;
        InGameManager.Ins.GetCardManager().TurnStart();
        CurrentTurnManagerUI.Ins.ShowPlayerTurnAnim();
        InGameManager.Ins.GetFieldManager().LaunchReady();
        InGameManager.Ins.GetEnemyManager().TurnProgression();
        InGameManager.Ins.GetPlayerInfoManager().TurnProgression();
        if (elapsedTurn == 2)
        {
            UI_Tutorial.Ins.ShowBattleTutorial_2();
        }
        InGameManager.Ins.GetRelicManager().TurnStartCheck(elapsedTurn);
    }
}
