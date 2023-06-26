using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyAttackEntryElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;

    public void Init(int pow, EnemyActionType actionType)
    {
        text.text = $"{pow}";
        switch (actionType)
        {
            case EnemyActionType.Attack:
                image.sprite = InGameManager.Ins.GetDatabase().GetAttackIconSprite();
                break;
            case EnemyActionType.Defence:
                image.sprite = InGameManager.Ins.GetDatabase().GetGuardIconSprite();
                break;
            case EnemyActionType.Quote:
                image.sprite = InGameManager.Ins.GetDatabase().GetQuestionIconSprite();
                text.text = "";
                break;
            case EnemyActionType.End:
                text.text = "";
                break;
            default:
                break;
        }
    }
}
