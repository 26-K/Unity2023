﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] public BattleCardBase cardBase; // カードのベースプレハブ
    public List<BattleCardBase> deck; // 山札のカードリスト
    public List<BattleCardBase> discardPile; // 捨て札のカードリスト
    public List<BattleCardBase> hand; // 手札のカードリスト
    [SerializeField] public HandManager handManager;
    [SerializeField] public DrawPileManager drawPlie;
    [SerializeField] public DiscardPileManager discardPileManager;
    [SerializeField] public ManaManager manaManager;
    [SerializeField] Canvas parentCanvas;
    // Start is called before the first frame update
    [HideInInspector] public InGameManager parent;
    public int mana { get; private set; } = 0;
    public int maxMana { get; private set; } = 3;

    public void Init(InGameManager parent)
    {
        this.parent = parent;
        handManager.Init(this);
        drawPlie.Init(this);
        discardPileManager.Init(this);
        manaManager.Init(this);
    }

    public void BattleStart()
    {
        maxMana = 3; //レリックの効果等で最大マナが変わるかもしれん、

        DestroyAllBattleCards(); //手札、山札、捨て札のカードを消す

        //現在のデッキのカードを山札に加える
        foreach (var b in parent.GetPlayerInfoManager().battleCardStatuses)
        {
            var card = Instantiate(cardBase, handManager.transform);
            var cardEffect = InGameManager.Ins.GetDatabase().GetCardData(b.id);
            cardEffect.Init(parent);
            card.Init(handManager.GetHandRect, parentCanvas, cardEffect);
            card.SetDrawPile();
            deck.Add(card);
        }
        ShuffleDeck();
        drawPlie.SelfUpdate();
    }

    private void DestroyAllBattleCards()
    {
        foreach (var a in deck)
        {
            Destroy(a);
        }

        foreach (var a in discardPile)
        {
            Destroy(a);
        }

        foreach (var a in hand)
        {
            Destroy(a);
        }
        deck.Clear(); // 山札のカードリスト
        discardPile.Clear(); // 捨て札のカードリスト
        hand.Clear(); // 手札のカードリスト
    }

    public void TurnStart()
    {
        int drawCount = 5;
        this.mana = 0;
        ChangeMana(maxMana);
        for (int i = 0; i < drawCount; i++)
        {
            DrawCard(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handManager.SelfUpdate();
        drawPlie.SelfUpdate();
        discardPileManager.SelfUpdate();
        DiscardCardCheck();

    }


    // 山札からカードを引く
    public void DrawCard(bool isTurnStartDraw = false)
    {
        if (deck.Count > 0)
        {
            BattleCardBase card = deck[0];
            deck.RemoveAt(0);
            hand.Add(card);
            AudioManager.Ins.PlayCardDrawSound();
            card.SetHandPile();
        }
        else
        {
            if (AssensionManager.Ins.GetIsNoDraw() == true && isTurnStartDraw == false)
            {
                return;
            }
            MoveDiscardPileToDeck();
            if (deck.Count > 0)
            {
                BattleCardBase card = deck[0];
                deck.RemoveAt(0);
                hand.Add(card);
                card.SetHandPile();
            }
        }
    }

    public void AllDiscard()
    {
        foreach (var a in hand)
        {
            if (a.GetNowPile() == NowPile.HandPile)
            {
                a.SetDiscard();
            }
        }
    }

    // カードを手札から捨て札に移動する
    public void DiscardCardCheck()
    {
        List<BattleCardBase> moveList = new List<BattleCardBase>();
        foreach (var a in hand)
        {
            if (a.GetNowPile() == NowPile.DiscardPile)
            {
                moveList.Add(a);
            }
        }
        if (moveList.Count <= 0)
        {
            return;
        }
        foreach (var a in moveList)
        {
            if (hand.Contains(a))
            {
                hand.Remove(a);
                discardPile.Add(a);
            }
        }
    }

    // 捨て札のカードを山札に戻す
    private void MoveDiscardPileToDeck()
    {
        foreach (var a in discardPile)
        {
            a.SetDiscardToDrawMoveNowPile(drawPlie.GetRect().localPosition);
        }
        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    // 山札をシャッフルする
    private void ShuffleDeck()
    {
        int deckSize = deck.Count;
        for (int i = 0; i < deckSize; i++)
        {
            BattleCardBase temp = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public bool IsEnoughMana(int requireMana)
    {
        return this.mana >= requireMana;
    }
    public void ChangeMana(int changeVal)
    {
        this.mana += changeVal;
        if (changeVal >= 1)
        {
            manaManager.PlayChargeManaAnim();
        }
        manaManager.Refresh();
        foreach (var a in hand)
        {
            a.Refresh();
        }
        foreach (var a in discardPile)
        {
            a.Refresh();
        }
        foreach (var a in deck)
        {
            a.Refresh();
        }
    }

    public void GenerateRandomCardAddHand()
    {
        var card = Instantiate(cardBase, handManager.transform);
        var cardEffect = GetRandom(InGameManager.Ins.GetDatabase().GetAllCards());
        cardEffect.Init(parent);
        card.Init(handManager.GetHandRect, parentCanvas, cardEffect);
        AddHand(card);
    }

    private void AddHand(BattleCardBase card)
    {
        card.SetHandPile();
        hand.Add(card);
    }

    T GetRandom<T>(List<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
