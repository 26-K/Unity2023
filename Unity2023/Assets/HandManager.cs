using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] List<BattleCardBase> cards;
    public float minSpacing = 100.0f; // 最小のカード間の間隔
    public float maxSpacing = 200.0f; // 最大のカード間の間隔
    public Transform cardStartPosition; // カードを配置する最初の位置
    float timer = 5.0f;
    [SerializeField] RectTransform rect;
    [SerializeField] Canvas canvas;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        foreach (var a in cards)
        {
            a.Init(rect, canvas);
        }
    }

    public void Update()
    {
        ArrangeCards();
    }

    private void ArrangeCards()
    {
        int cardCount = cards.Count;
        float totalSpacing = Mathf.Clamp((cardCount - 1) * minSpacing, 0f, (cardCount - 1) * maxSpacing); // カード間の総間隔を計算
        float startX = cardStartPosition.localPosition.x - totalSpacing / 2f; // カードを配置する最初の位置のX座標
        float spacing = Mathf.Lerp(maxSpacing, minSpacing, (cardCount - 1) / 10f); // カードの枚数に応じて間隔を計算
        for (int i = 0; i < cardCount; i++)
        {
            Vector3 position = new Vector3(startX + i * spacing, cardStartPosition.position.y, cardStartPosition.position.z);
            cards[i].SetTargetPos(position);
        }
    }
}
