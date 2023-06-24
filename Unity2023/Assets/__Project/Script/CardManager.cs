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
    }

    public void TurnStart()
    {
        int drawCount = 5;
        this.mana = 0;
        ChangeMana(maxMana);
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
            GenerateRandomCardAddHand();
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
    }

    public void GenerateRandomCardAddHand()
    {
        var card = Instantiate(cardBase, handManager.transform);
        var cardEffect = GetRandom(InGameManager.Ins.GetDatabase().GetAllCards());
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
