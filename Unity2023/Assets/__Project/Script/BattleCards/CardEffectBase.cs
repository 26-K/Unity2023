using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardEffectBase : ScriptableObject
{
    public SetObjectBase model;
    [LabelText("設置物かどうか")] public bool isSet = true;
    public int id = 0;
    [LabelText("レアリティ")] public Rarity rarity = Rarity.Normal;
    [LabelText("カード画像")] public Sprite sprite;
    [LabelText("コスト")] public int cost = 1;
    [LabelText("カード名")] public string cardName = "";
    [LabelText("カードテキスト")] public string cardText = "";
    protected InGameManager parent;
    public void Init(InGameManager parent) { this.parent = parent; }

    public void DoUse()
    {
        if (CanUse() == false)
        {
            Debug.Log($"コスト不足:\n 現在:{InGameManager.Ins.GetCardManager().IsEnoughMana(cost)}/必要{cost}");
            return;
        }
        else
        {
            UseEffect();
            InGameManager.Ins.GetCardManager().ChangeMana(-cost);

        }
    }

    public bool CanUse()
    {
        return InGameManager.Ins.GetCardManager().IsEnoughMana(cost);
    }

    public virtual void UseEffect()
    {
        // マウスの位置を取得
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // オブジェクトの奥行き（Z軸）の位置を設定

        // マウスの位置をワールド座標に変換
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // オブジェクトを配置する位置を基準にするためのオフセットを設定
        Vector3 offset = new Vector3(0, 0, 0); // 適切なオフセットを設定してください

        // オブジェクトを配置する位置を計算
        Vector3 placementPosition = worldPosition + offset;

        // オブジェクトを配置する位置に移動させる
        model.transform.position = placementPosition;

        var a = Instantiate(model, placementPosition, Quaternion.identity);

        InGameManager.Ins.GetFieldManager().SetObject(a);
        AudioManager.Ins.PlaySetObjectSound();
        //a.transform.localPosition = new Vector3(a.transform.position.x, a.transform.position.y, 0);
       
    }

    public void ShowModel(Vector3 pos)
    {
        //if (isSet && )
        //{

        //}
    }
}
