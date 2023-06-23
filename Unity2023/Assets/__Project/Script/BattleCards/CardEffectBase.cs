﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardEffectBase : ScriptableObject
{
    public GameObject model;
    [LabelText("設置物かどうか")]public bool isSet = true;
    public int id = 0;
    [LabelText("レアリティ")]public Rarity rarity = Rarity.Normal;
    [LabelText("カード画像")]public Sprite sprite;
    [LabelText("カード名")]public int cost = 1;
    [LabelText("カード名")]public string cardName = "";
    [LabelText("カードテキスト")]public string cardText = "";
    protected InGameManager parent;
    public void Init(InGameManager parent) { this.parent = parent; }

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

        // 左クリックでオブジェクトを配置する
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(model, placementPosition, Quaternion.identity);
        }
    }

    public void ShowModel(Vector3 pos)
    {
        //if (isSet && )
        //{

        //}
    }
}