using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    PlayerTurn_Wait,
    EnemyTurn,
}

public class TurnManager : SingletonMonoBehaviour<TurnManager>
{
    public TurnState currentTurn;
    protected override void UnityAwake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FinishEnemyTurn();
        }
    }

    public void PushTurnEndButton()
    {
        if (currentTurn != TurnState.PlayerTurn)
        {
            return;
        }
        InGameManager.Ins.GetCardManager().DiscardCardCheck();
        currentTurn = TurnState.PlayerTurn_Wait;
    }

    public void FinishPlayerTurn()
    {
        currentTurn = TurnState.EnemyTurn;
        CurrentTurnManagerUI.Ins.ShowEnemyTurnAnim();
    }

    public void FinishEnemyTurn()
    {
        currentTurn = TurnState.PlayerTurn;
        CurrentTurnManagerUI.Ins.ShowPlayerTurnAnim();
    }
}
