using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    InGameManager parent;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI floorText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinText;
    float updateTime = 0.0f;
    float alphaVal = 0.0f;
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
            //coinText.text = $"{0}";
        }

        //キーボードのSが押されている間はスコア表示
        if (Input.GetKeyDown(KeyCode.S))
        {
            alphaVal = 1.0f;
            scoreText.transform.gameObject.SetActive(true);
            int val = ScoreManager.Ins.GetNowScore();
            scoreText.text = $"Score {val}";
        }
        alphaVal -= Time.deltaTime;
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, alphaVal);
    }
}
