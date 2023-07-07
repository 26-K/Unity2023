using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEventManager : MonoBehaviour
{
    [SerializeField] List<DungeonEventButton> dungeonEventButtons;
    [SerializeField] GameObject sizeObj;

    public void Show()
    {
        sizeObj.SetActive(true);
        foreach (var a in dungeonEventButtons)
        {
            a.Init();
        }
    }

    public void PushButton(int id)
    {
        foreach (var a in dungeonEventButtons)
        {
            a.transform.gameObject.SetActive(false);
        }
    }

    public void PushCloseButton()
    {
        sizeObj.SetActive(false);
    }
}
