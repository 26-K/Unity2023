using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_AllDelete : CardEffectBase
{
    public override void UseEffect()
    {
        parent.GetFieldManager().ResetField();
    }
}
