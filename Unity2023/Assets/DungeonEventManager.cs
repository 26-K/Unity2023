using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEventManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] Image image;
    [SerializeField] List<DungeonEventButton> dungeonEventButtons;
    [SerializeField] GameObject sizeObj;
    [SerializeField] GameObject closeButton;
    EventBase ev;

    public void Show()
    {
        sizeObj.SetActive(true);
        this.ev = LotteryEvent();
        titleText.text = this.ev.title;
        descText.text = this.ev.desc;
        image.sprite = ev.imageSprite;
        int index = 0;
        closeButton.SetActive(false);
        foreach (var a in dungeonEventButtons)
        {
            a.gameObject.SetActive(false);
        }
            foreach (var a in dungeonEventButtons)
        {
            index++;
            a.transform.gameObject.SetActive(true);
            a.Init(index, ev);
            if (index >= ev.selectCount)
            {
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Show();
        }
    }

    public void PushButton(int index)
    {
        foreach (var a in dungeonEventButtons)
        {
            a.transform.gameObject.SetActive(false);
        }
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {

        }).OnComplete(() =>
        {
            closeButton.SetActive(true);
            switch (index)
            {
                case 1:
                    descText.text = ev.afterSelect_1;
                    ev.Select_1();
                    break;
                case 2:
                    descText.text = ev.afterSelect_2;
                    ev.Select_2();
                    break;
                case 3:
                    descText.text = ev.afterSelect_3;
                    ev.Select_3();
                    break;
                default:
                    break;
            }
        }
        );
    }

    public void PushCloseButton()
    {
        sizeObj.SetActive(false);
        InGameManager.Ins.NextFloor();
    }

    public EventBase LotteryEvent()
    {
        return InGameManager.Ins.GetDatabase().GetRandomEvent();
    }
}
