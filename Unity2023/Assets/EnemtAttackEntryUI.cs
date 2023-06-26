using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtAttackEntryUI : MonoBehaviour
{
    [SerializeField] UI_EnemyAttackEntryElement ui_EnemyAttack;
    [SerializeField] GameObject instParent;
    [SerializeField] List<UI_EnemyAttackEntryElement> ui_EnemyAttacks = new List<UI_EnemyAttackEntryElement>();
    public void Init(List<EnemyAction> enemyActions)
    {
        foreach (var a in ui_EnemyAttacks)
        {
            Destroy(a.transform.gameObject);
        }
        ui_EnemyAttacks.Clear();

        foreach (var a in enemyActions)
        {
            if (a.actionType != EnemyActionType.End)
            {
                var b = Instantiate(ui_EnemyAttack, instParent.transform);
                b.Init(a.actionPow, a.actionType);
                ui_EnemyAttacks.Add(b);
            }
        }
    }

    public void Hide()
    {
        foreach (var a in ui_EnemyAttacks)
        {
            Destroy(a.transform.gameObject);
        }
        ui_EnemyAttacks.Clear();
    }
}
