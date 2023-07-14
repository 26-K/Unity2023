using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NowDeck : MonoBehaviour
{
    List<BattleCardBase> battleCards = new List<BattleCardBase>();
    [SerializeField] GameObject sizeObj;
    [SerializeField] GameObject contentObj;
    [SerializeField] BattleCardBase battleCardBase;
    [SerializeField] ScrollRect scrollRect;

    public void InitShow()
    {
        foreach (var a in battleCards)
        {
            Destroy(a.transform.gameObject);
        }
        battleCards.Clear();
        sizeObj.SetActive(true);
        foreach (var b in InGameManager.Ins.GetPlayerInfoManager().battleCardStatuses)
        {
            var card = Instantiate(battleCardBase, contentObj.transform);
            var cardEffect = InGameManager.Ins.GetDatabase().GetCardData(b.id);
            cardEffect.Init(InGameManager.Ins);
            card.Init(cardEffect);
            card.SetStatus(b);
            card.SetOtherMode(OtherCardMode.NoneMode);
            card.isNoTouchMode = true;
            battleCards.Add(card);
        }
    }
    public void PushUpButton()
    {
        scrollRect.velocity -= new Vector2(0, 400.0f);
    }

    public void PushDownButton()
    {
        scrollRect.velocity += new Vector2(0, 400.0f);
    }

    public void PushCloseButton()
    {
        sizeObj.SetActive(false);
    }
}
