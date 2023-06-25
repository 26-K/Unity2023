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
        CurrentTurnManagerUI.Ins.ShowBattleStartAnim();
        InGameManager.Ins.GetCardManager().BattleStart();
        DOVirtual.DelayedCall(1.5f, () =>
        {
            StartPlayerTurn();
        }
        );
    }

    /// <summary>
    /// ターン終了時の処理
    /// </summary>
    public void PushTurnEndButton()
    {
        PlayerTurnEnd();
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

    public void AllFinishPlayerTurn()
    {
        currentTurn = TurnState.EnemyTurn;
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
    }
}
