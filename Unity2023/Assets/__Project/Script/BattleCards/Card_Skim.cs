using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Skim : CardEffectBase
{
    public override void UseEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            parent.GetCardManager().DrawCard();
        }
    }
}
