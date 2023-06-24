using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] public BattleCardBase cardBase; // 山札のカードリスト
    public List<BattleCardBase> deck; // 山札のカードリスト
    public List<BattleCardBase> discardPile; // 捨て札のカードリスト
    public List<BattleCardBase> hand; // 手札のカードリスト
    [SerializeField] public HandManager handManager;
    [SerializeField] public DrawPileManager drawPlie;
    [SerializeField] public DiscardPileManager discardPileManager;
    // Start is called before the first frame update
    public InGameManager parent;

    public void Init(InGameManager parent)
    {
        this.parent = parent;
        handManager.Init(this);
        drawPlie.Init(this);
        discardPileManager.Init(this);
    }

    public void BattleStart()
    {
        int drawCount = 5;
        for (int i = 0; i < drawCount; i++)
        {
            DrawCard();
        }
    }

    public void TurnStart()
    {
        int drawCount = 5;
        for (int i = 0; i < drawCount; i++)
        {
            DrawCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        handManager.SelfUpdate();
        drawPlie.SelfUpdate();
        discardPileManager.SelfUpdate();
        DiscardCardCheck();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveDiscardPileToDeck();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawCard();
        }
    }


    // 山札からカードを引く
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            BattleCardBase card = deck[0];
            deck.RemoveAt(0);
            hand.Add(card);
            card.SetHandPile();
        }
        else
        {
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
}
