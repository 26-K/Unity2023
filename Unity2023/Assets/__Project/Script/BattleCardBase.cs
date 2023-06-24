using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum NowPile
{
    DrawPile,
    HandPile,
    DiscardPile,
    DiscardToDrawMoveNowPile,
}
public class BattleCardStatus
{
    public int id = 0;
    public int level = 0;
}
public class BattleCardBase : MonoBehaviour
{
    public bool isPush;
    public bool isSelect;
    public bool isUse = false;
    [SerializeField] RectTransform sizeRect;
    [SerializeField] Image backRarityImage;
    [SerializeField] TextMeshProUGUI cardNameText;
    [SerializeField] TextMeshProUGUI cardDescText;
    [SerializeField] TextMeshProUGUI costText;
    Vector2 targetPos = new Vector2(0, 0.0f);
    Vector2 currentPos = new Vector2(0, 0.0f);
    Vector2 targetScale = new Vector2(1.0f, 1.0f);
    Vector2 currentScale = new Vector2(1.0f, 1.0f);

    NowPile nowPile = NowPile.HandPile;
    //float targetPos = 10.0f;
    //float currentPos = 0.0f;
    RectTransform parentRect;
    Canvas canvas;
    Vector2 margin = new Vector2(0.0f, 0.0f);
    Vector2 marginCard = new Vector2(0.0f, -200.0f);
    float drawPileWaitTimer = 0.0f;

    public void Init(RectTransform rect, Canvas parentCanvas)
    {
        this.parentRect = parentCanvas.GetComponent<RectTransform>();
        margin = parentRect.sizeDelta * 0.5f;
        this.canvas = parentCanvas;
    }

    public void UpdateParent(RectTransform parentCanvasRect, Canvas canvas)
    {
        this.parentRect = parentCanvasRect;
        margin = parentRect.sizeDelta * 0.5f;
        this.canvas = canvas;
    }
    private void Update()
    {
        drawPileWaitTimer -= Time.deltaTime;
        if (nowPile == NowPile.DrawPile)
        {
        }
        else if (nowPile == NowPile.HandPile)
        {
            HandPile();
        }
        else if (nowPile == NowPile.DiscardPile)
        {
        }
        else if (nowPile == NowPile.DiscardToDrawMoveNowPile)
        {
        }
        currentPos = Vector2.Lerp(currentPos, targetPos, 0.1f);
        sizeRect.anchoredPosition = currentPos;
        currentScale = Vector3.Lerp(currentScale, targetScale, 0.1f);
        sizeRect.localScale = currentScale;

    }

    private void HandPile()
    {
        if (isPush)
        {
            Vector3 mousePosition = GetMouseCanvasPosition();
            mousePosition.x += -margin.x + marginCard.x;
            mousePosition.y += marginCard.y;
            currentPos = mousePosition;
            targetScale = Vector3.one * 0.5f;

        }
        else
        {
            if (isSelect)
            {
                targetPos.y = 30.0f;
                targetScale = Vector3.one * 1.05f;
            }
            else
            {
                targetPos.y = 0.0f;
                targetScale = Vector3.one * 1.0f;
            }
        }
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void OnPointerEnter()
    {
        AudioManager.Ins.PlayCardHoverSound();
        isSelect = true;
    }


    public void OnPointerExit()
    {
        isSelect = false;
    }

    public void OnPointerDown()
    {
        isPush = true;
    }

    public void OnPointerUp()
    {
        isPush = false;

        Vector3 mousePosition = Input.mousePosition;

        // マウスの位置をワールド座標に変換
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Raycastを実行して3Dオブジェクトとの衝突を判定
        if (Physics.Raycast(ray, out hit))
        {
            // 衝突したオブジェクトが特定のタグを持つかチェック
            if (hit.collider.CompareTag("BattleField"))
            {
                // 重なっているので使用可能
                isUse = true;
                Debug.Log("Use");
            }
        }

        if (isUse == true)
        {
            nowPile = NowPile.DiscardPile;
        }
    }
    private Vector3 GetMouseCanvasPosition()
    {
        Vector3 mousePosition;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(parentRect, Input.mousePosition, canvas.worldCamera, out mousePosition))
        {
            return mousePosition;
        }

        return transform.position;
    }

    public void SetHandPile()
    {
        nowPile = NowPile.HandPile;
    }
    public void SetDiscard(Vector2 discardPliePos)
    {
        nowPile = NowPile.DiscardPile;
        this.targetPos = discardPliePos;
        this.targetPos.y = 0;
        this.targetScale = Vector2.zero;
    }
    public void SetDiscardToDrawMoveNowPile(Vector2 discardPliePos)
    {
        drawPileWaitTimer = 0.5f;
        nowPile = NowPile.DiscardToDrawMoveNowPile;
        this.targetPos = discardPliePos;
        this.targetPos.y = 0;
        this.targetScale = Vector2.one * 0.3f;
    }
    public void SetDrawPile(Vector2 drawPliePos)
    {
        if (drawPileWaitTimer > 0)
        {
            return;
        }
        nowPile = NowPile.DrawPile;
        this.targetPos = drawPliePos;
        this.targetPos.y = 0;
        this.targetScale = Vector2.zero;
    }

    public NowPile GetNowPile()
    {
        return this.nowPile;
    }
}
