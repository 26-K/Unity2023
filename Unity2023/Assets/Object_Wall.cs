using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Wall : MonoBehaviour, IObject
{
    public void HitObject()
    {
        Debug.Log("壁Hit");
        InGameManager.Ins.GetRelicManager().WallHitEnableCheck();
    }
}
