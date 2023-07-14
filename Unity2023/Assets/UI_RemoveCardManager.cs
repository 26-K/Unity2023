using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RemoveCardManager : SingletonMonoBehaviour<UI_RemoveCardManager>
{
    [SerializeField] List<BattleCardBase> battleCards = new List<BattleCardBase>();
    [SerializeField] GameObject sizeObj;
    [SerializeField] GameObject size2_Obj;
    [SerializeField] GameObject contentObj;
    [SerializeField] BattleCardBase battleCardBase;
    [SerializeField] ScrollRect scrollRect;
    
    [SerializeField] GameObject removeTargetParent;
    BattleCardBase removeTarget;
    protected override void UnityAwake()
    {
    }

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
            card.SetOtherMode(OtherCardMode.RemoveMode);
            battleCards.Add(card);
        }
    }

    public void PushRemoveCard(BattleCardStatus battleCardStatus)
    {
        if (removeTarget != null)
        {
            Destroy(removeTarget.gameObject);
        }
        sizeObj.SetActive(false);
        size2_Obj.SetActive(true);
        var card = Instantiate(battleCardBase, removeTargetParent.transform);
        var cardEffect = InGameManager.Ins.GetDatabase().GetCardData(battleCardStatus.id);
        card.Init(cardEffect);
        card.isNoTouchMode = true;
        card.SetStatus(battleCardStatus);
        removeTarget = card;
    }

    public void PushUpButton()
    {
        scrollRect.velocity -= new Vector2(0, 400.0f);
    }

    public void PushDownButton()
    {
        scrollRect.velocity += new Vector2(0, 400.0f);
    }

    public void PushRemoveButton()
    {
        sizeObj.SetActive(false);
        size2_Obj.SetActive(false);
        InGameManager.Ins.GetPlayerInfoManager().RemoveCardInDeck(removeTarget.GetStatus());
    }

    public void PushCancelButton()
    {
        sizeObj.SetActive(true);
        size2_Obj.SetActive(false);
    }
}
