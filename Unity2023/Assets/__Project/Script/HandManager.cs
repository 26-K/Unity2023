using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public float minSpacing = 100.0f; // 最小のカード間の間隔
    public float maxSpacing = 200.0f; // 最大のカード間の間隔
    public Transform cardStartPosition; // カードを配置する最初の位置
    [SerializeField] RectTransform rect;
    public RectTransform GetHandRect => rect;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] float topMargin = 3.0f;
    CardManager parent;
    private void Start()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
    }
    public void Init(CardManager parent)
    {
        this.parent = parent;
    }

    public void SelfUpdate()
    {
        ArrangeCards();
    }

    private void ArrangeCards()
    {
        int cardCount = parent.hand.Count;
        float totalSpacing = Mathf.Clamp((cardCount - 1) * minSpacing, 0f, (cardCount - 1) * maxSpacing); // カード間の総間隔を計算
        float startX = cardStartPosition.localPosition.x - totalSpacing / 2.0f + minSpacing; // カードを配置する最初の位置のX座標
        float spacing = Mathf.Lerp(maxSpacing, minSpacing, (cardCount - 1) / 10f); // カードの枚数に応じて間隔を計算
        for (int i = 0; i < cardCount; i++)
        {
            Vector3 position = new Vector3(startX + i * spacing, cardStartPosition.position.y, cardStartPosition.position.z);
            Vector3 addPosition = Vector3.zero;
            addPosition.y = Mathf.Abs(i - (cardCount* 0.5f)) * topMargin;
            parent.hand[i].UpdateParent(canvasRect, canvas);
            parent.hand[i].SetTargetPos(position);
            parent.hand[i].SetAddPos(addPosition);
        }
    }

}
