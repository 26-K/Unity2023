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
    public TurnState GetCurrentTurn => currentTurn; //経過ターン
    int elapsedTurn = 0;
    public int GetElapsedTurn => elapsedTurn; //経過ターン
    protected override void UnityAwake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            BattleStart();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FinishEnemyTurn();
        }
    }

    public void BattleStart()
    {
        elapsedTurn = 0;
        CurrentTurnManagerUI.Ins.ShowBattleStartAnim();
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
        InGameManager.Ins.GetCardManager().DiscardCardCheck();
        currentTurn = TurnState.PlayerTurn_Wait;
        InGameManager.Ins.GetFieldManager().LaunchStart();
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
    }
}
