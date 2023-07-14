using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBase : ScriptableObject
{
    [LabelText("タイトル")] public string title = "";
    [Multiline(4)] [LabelText("説明")] public string desc = "";
    [PreviewField(200)] [LabelText("画像")] public Sprite imageSprite;
    [LabelText("選択肢の数")] public int selectCount = 3;
    [LabelText("選択肢1")] public string select_1 = "";
    [LabelText("選択肢2")] public string select_2 = "";
    [LabelText("選択肢3")] public string select_3 = "";


    [Multiline(3)] [LabelText("1を選択後")] public string afterSelect_1 = "";
    [Multiline(3)] [LabelText("2を選択後")] public string afterSelect_2 = "";
    [Multiline(3)] [LabelText("3を選択後")] public string afterSelect_3 = "";
    public virtual void Select_1()
    {

    }
    public virtual void Select_2()
    {

    }
    public virtual void Select_3()
    {

    }
}
