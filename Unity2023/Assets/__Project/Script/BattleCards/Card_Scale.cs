using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Scale : CardEffectBase
{
    public override void UseEffect()
    {
        AudioManager.Ins.PlayOneMoreSound();
        InGameManager.Ins.GetFieldManager().ScaleGate();
    }
}
