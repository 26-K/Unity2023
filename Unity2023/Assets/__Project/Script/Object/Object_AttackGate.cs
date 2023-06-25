using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_AttackGate : SetObjectBase
{
    public override void TriggerEnterAction(Collider collider)
    {
        var a = collider.GetComponent<BulletBase>();
        if (a != null)
        {
            InGameManager.Ins.GetEnemyManager().AddDamage(a.CalcPow());
        }
        AudioManager.Ins.PlayGateInSound();
        var randval = Random.Range(0, 3);
        if (randval == 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, "ナイス!");
        }
        else if (randval == 1)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, "OK!");
        }
        else
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, "Good!");
        }
    }
}
