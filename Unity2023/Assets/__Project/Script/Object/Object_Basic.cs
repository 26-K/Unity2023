using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Basic : SetObjectBase
{
    [LabelText("弾が触れた時に鳴らす効果音タイプ")]public ObjectHitType hitPlaySoundType;
    public override void TouchEnterAction(Collision collision)
    {
    }
}
