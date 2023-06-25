using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Zuru : CardEffectBase
{
    public override void UseEffect()
    {
        AudioManager.Ins.PlayOneMoreSound();
        for (int i = 0; i < 2; i++)
        {
            parent.GetCardManager().DrawCard();
        }
        parent.GetCardManager().ChangeMana(1);
    }
}
