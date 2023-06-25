using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    InGameManager parent;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI floorText;
    float updateTime = 0.0f;
    public void Init(InGameManager parent)
    {
        this.parent = parent;
    }

    private void Update()
    {
        updateTime += Time.deltaTime;
        if (updateTime >= 1.0f) //雑に1秒ごと更新
        {
            floorText.text = $"{parent.GetPlayerInfoManager().floor}F";
            hpText.text = $"{parent.GetPlayerInfoManager().hp}/{parent.GetPlayerInfoManager().maxHp}";
        }
    }
}
