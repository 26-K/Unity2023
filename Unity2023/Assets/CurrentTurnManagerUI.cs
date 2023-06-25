using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTurnManagerUI : SingletonMonoBehaviour<CurrentTurnManagerUI>
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] GameObject sizeObj;
    protected override void UnityAwake()
    {
        sizeObj.SetActive(false);
    }
    public void ShowBattleStartAnim()
    {
        AudioManager.Ins.PlayBattleStartSound();
        sizeObj.SetActive(true);
        anim.Play("BattleStart");
        turnText.text = "Battle Start";
        currentTurnText.transform.gameObject.SetActive(false);
    }

    public void ShowPlayerTurnAnim()
    {
        AudioManager.Ins.PlayTurnStartSound();
        sizeObj.SetActive(true);
        anim.Play("TurnStart");
        turnText.text = "Player Turn";
        currentTurnText.transform.gameObject.SetActive(true);
        currentTurnText.text = $"Turn {TurnManager.Ins.GetElapsedTurn}";
    }
    public void ShowEnemyTurnAnim()
    {
        sizeObj.SetActive(true);
        currentTurnText.transform.gameObject.SetActive(false);
        anim.Play("TurnStart");
        turnText.text = "Enemy Turn";
    }
}
