using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Basic : SetObjectBase,IObject
{
    [LabelText("弾が触れた時に鳴らす効果音タイプ")]public ObjectHitType hitPlaySoundType;
    [LabelText("弾が触れた時に鳴らす効果音タイプ")]public string objectName;


    public void HitObject()
    {
        InGameManager.Ins.GetRelicManager().ObjectHitCheck(objectName);
    }

    public override void TouchEnterAction(Collision collision)
    {
        if (hitPlaySoundType == ObjectHitType.Nail)
        {
            AudioManager.Ins.PlayNailHitSound();
        }
        if (hitPlaySoundType == ObjectHitType.Wall)
        {
            AudioManager.Ins.PlayWoodHitSound();
        }
    }
    public override void TriggerEnterAction(Collider collider)
    {
        if (hitPlaySoundType == ObjectHitType.Nail)
        {
            AudioManager.Ins.PlayNailHitSound();
        }
        if (hitPlaySoundType == ObjectHitType.Wall)
        {
            AudioManager.Ins.PlayWoodHitSound();
        }
    }
}
