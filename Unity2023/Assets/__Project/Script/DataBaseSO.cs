﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseSO : ScriptableObject
{
    [SerializeField]
    public List<Sprite> rarityImages;
    public List<CardEffectBase> cards;
    [LabelText("初期デッキ")]public List<CardEffectBase> firstCards = new List<CardEffectBase>();
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
}

public enum Rarity
{
    None,
    Normal,
    Rare,
    SuperRare
}