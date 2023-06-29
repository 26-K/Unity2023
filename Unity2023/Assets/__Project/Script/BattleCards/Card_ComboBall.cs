using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_ComboBall : CardEffectBase
{
    public override void UseEffect()
    {
        AudioManager.Ins.PlayOneMoreSound();
        InGameManager.Ins.GetFieldManager().AddLaunchPos().AddCombo(3);
    }
}
