using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardEffectBase : ScriptableObject
{
    public GameObject model;
    public int id = 0;
    [LabelText("レアリティ")]public Rarity rarity = Rarity.Normal;
    [LabelText("カード画像")]public Sprite sprite;
    [LabelText("カード名")]public int cost = 1;
    [LabelText("カード名")]public string cardName = "";
    [LabelText("カードテキスト")]public string cardText = "";
    protected InGameManager parent;
    public void Init(InGameManager parent) { this.parent = parent; }

    public virtual void UseEffect() { }
}
