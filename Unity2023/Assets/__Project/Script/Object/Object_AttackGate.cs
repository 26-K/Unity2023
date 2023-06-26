using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_AttackGate : SetObjectBase
{
    public override void TriggerEnterAction(Collider collider)
    {
        var a = collider.GetComponent<BulletBase>();
        int addVal = 0;
        if (a != null)
        {
            addVal = a.CalcPow();
            a.ResetCombo();
            InGameManager.Ins.GetEnemyManager().AddDamage(addVal);
        }
        else
        {
            return;
        }
        AudioManager.Ins.PlayGateInSound();
        var randval = Random.Range(0, 3);
        if (randval == 0)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, $"ナイス!\n+{addVal}");
        }
        else if (randval == 1)
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, $"OK!\n+{addVal}");
        }
        else
        {
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, $"Good!\n+{addVal}");
        }
    }
}
