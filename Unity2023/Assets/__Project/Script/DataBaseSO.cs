using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseSO : ScriptableObject
{
    [SerializeField]
    public EnemyDataBaseSo enemyDataBaseSo;
    public List<Sprite> rarityImages;
    public List<CardEffectBase> cards;
    [LabelText("初期デッキ")]public List<CardEffectBase> firstCards = new List<CardEffectBase>();
    [SerializeField] Sprite attackIconImage;
    [SerializeField] Sprite guardIconImage;
    [SerializeField] Sprite questionIconImage;
    public Sprite GetCardBackImage(int rarity)
    {
        if (rarity > rarityImages.Count)
        {
            rarity = rarityImages.Count;
        }
        return rarityImages[rarity - 1];
    }

    public List<CardEffectBase> GetAllCards()
    {
        return cards;
    }

    public CardEffectBase GetCardData(int id)
    {
        return cards.Find(x => x.id == id);
    }
    public EnemyBase GetRandomEnemy(int floor)
    {
        List<EnemyBase> enemys = new List<EnemyBase>();
        foreach (var item in enemyDataBaseSo.enemyDatas)
        {
            if (item.minFloor <= floor && floor <= item.maxFloor)
            {
                enemys.Add(item.enemy);
            }
        }
        return GetRandom(enemys);
    }
    T GetRandom<T>(List<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }

    public Sprite GetAttackIconSprite()
    {
        return attackIconImage;

    }
    public Sprite GetGuardIconSprite()
    {
        return guardIconImage;

    }
    public Sprite GetQuestionIconSprite()
    {
        return questionIconImage;

    }
}

public enum Rarity
{
    None,
    Starter,
    Normal,
    Rare,
    SuperRare
}