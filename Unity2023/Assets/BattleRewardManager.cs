using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRewardManager : MonoBehaviour
{
    public List<CardEffectBase> rewardCards = new List<CardEffectBase>();
    public GameObject obj;
    public GameObject darkObj;
    public List<BattleCardBase> battleCardObjects = new List<BattleCardBase>();

    private void Start()
    {
        obj.SetActive(false);
        darkObj.SetActive(false);
    }
    public void ShowAndLotteryRewardCards(int cnt = 3,Rarity rarity = Rarity.None)
    {
        obj.SetActive(true);
        darkObj.SetActive(true);
        rewardCards.Clear();
        Rarity rare = Rarity.Normal;
        for (int i = 0; i < cnt; i++)
        {
            if (rarity == Rarity.None)
            {
                rare = LotteryRarity();
            }
            else
            {
                rare = rarity;
            }
            var allCards = InGameManager.Ins.GetDatabase().GetAllCards().FindAll(x => x.rarity == rare);
            rewardCards.Add(GetRandom(allCards));
        }

        int cnts = 0;
        Vector2 baseInst = new Vector2(-300, 0);
        foreach (var a in rewardCards)
        {
            var b = Instantiate(InGameManager.Ins.GetCardManager().cardBase, obj.transform);
            b.Init(a);
            b.SetOtherMode(OtherCardMode.RewardMode);
            b.transform.DOLocalMoveX(baseInst.x + (cnts * 300), 0.5f, true);
            battleCardObjects.Add(b);

            cnts++;
        }
    }

    public void HideAndNextFloor()
    {
        InGameManager.Ins.NextFloor();
    }

    public void Hide()
    {
        foreach (var a in battleCardObjects)
        {
            Destroy(a.transform.gameObject);
        }
        battleCardObjects.Clear();
        obj.SetActive(false);
        darkObj.SetActive(false);
    }

    T GetRandom<T>(List<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }

    /// <summary>
    /// カードのレアリティ毎の出現率を計算する
    /// スターター:5%
    /// ノーマル :50%
    /// レア :37%
    /// 凄いレア : 8%
    /// </summary>
    /// <returns></returns>
    public Rarity LotteryRarity()
    {
        int randVal = Random.Range(0, 100);

        randVal -= 25;//スターター
        if (randVal <= 0)
        {
            return Rarity.Starter;
        }
        randVal -= 33;//ノーマル
        if (randVal <= 0)
        {
            return Rarity.Normal;
        }
        randVal -= 32;//レア
        if (randVal <= 0)
        {
            return Rarity.Rare;
        }
        return Rarity.SuperRare;
    }
}
