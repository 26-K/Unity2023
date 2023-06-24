using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTurnManagerUI : SingletonMonoBehaviour<CurrentTurnManagerUI>
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] GameObject sizeObj;
    protected override void UnityAwake()
    {
        sizeObj.SetActive(false);
    }

    public void ShowPlayerTurnAnim()
    {
        sizeObj.SetActive(true);
        anim.Play("TurnStart");
        turnText.text = "Player Turn";
    }
    public void ShowEnemyTurnAnim()
    {
        sizeObj.SetActive(true);
        anim.Play("TurnStart");
        turnText.text = "Enemy Turn";
    }
}
