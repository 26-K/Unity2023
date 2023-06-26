using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Animator anim;
    [SerializeField] ScoreUI score;
    [SerializeField] TextMeshProUGUI clearText;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] GameObject titleButton;
    [SerializeField] GameObject deckPos;
    [SerializeField] BattleCardBase battleCardBase;
    private void Start()
    {
        obj.SetActive(false);
    }
    public void ShowGameOverUI()
    {
        obj.SetActive(true);
        anim.Play("GameOver");
        DOVirtual.DelayedCall(2.0f, () =>
        {
            score.gameObject.SetActive(true);
            score.Init();
            titleButton.SetActive(true);
            ShowDeckCard();
        });
    }
    public void ShowGameClearUI()
    {
        obj.SetActive(true);
        clearText.text = "GameClear!";
        anim.Play("GameOver");
        DOVirtual.DelayedCall(2.0f, () =>
        {
            score.gameObject.SetActive(true);
            score.Init();
            titleButton.SetActive(true);
            ShowDeckCard();
        });
        DOVirtual.DelayedCall(9.0f, () =>
        {
            gameEndText.transform.gameObject.SetActive(true);
        });
    }

    public void PushTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ShowDeckCard()
    {
        float cnt = 0.0f;
        foreach (var b in InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses)
        {
            DOVirtual.DelayedCall(cnt, () =>
             {
                 var card = Instantiate(battleCardBase, deckPos.transform);
                 var cardEffect = InGameManager.Ins.GetDatabase().GetCardData(b.id);
                 cardEffect.Init(InGameManager.Ins);
                 card.Init(cardEffect);
                 card.gameObject.transform.localScale = Vector3.zero;
                 card.gameObject.transform.DOScale(Vector3.one, 0.35f);
             });
            cnt += 0.15f;
        }
    }
}
