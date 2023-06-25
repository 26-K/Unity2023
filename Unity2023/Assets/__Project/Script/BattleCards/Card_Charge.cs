using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Charge : CardEffectBase
{
    public override void UseEffect()
    {
        AudioManager.Ins.PlayOneMoreSound();
        parent.GetCardManager().ChangeMana(2);
    }
}
