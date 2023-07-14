using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonEventButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DungeonEventManager parent;
    int index = 1;

    public void Init(int index, EventBase eventBase)
    {
        this.index = index;
        switch (index)
        {
            case 1:
                text.text = eventBase.select_1;
                break;
            case 2:
                text.text = eventBase.select_2;
                break;
            case 3:
                text.text = eventBase.select_3;
                break;
            default:
                break;
        }
        
    }

    public void PushButton()
    {
        parent.PushButton(index);
    }
}
