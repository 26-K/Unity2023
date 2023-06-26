using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_DupliGate : SetObjectBase
{
    float coolTimer = 0.0f;

    public override void SelfUpdate()
    {
        coolTimer += Time.deltaTime;
    }

    public override void SelfStart()
    {
        coolTimer = 10.0f;
    }
    public override void TriggerEnterAction(Collider collider)
    {
        if (coolTimer < 1.0f)
        {
            return;
        }
        coolTimer = 0.0f;
        var a = collider.GetComponent<BulletBase>();
        if (a != null)
        {
            InGameManager.Ins.GetFieldManager().AddLaunch(a.transform.position);
        }
        AudioManager.Ins.PlayDuplicateSound();

        InGameManager.Ins.GetUI_PopUpManager().ShowPopUpText(collider.transform, $"複製!");
    }
}
