using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardBase : MonoBehaviour
{
    public bool isPush;
    public bool isSelect;
    [SerializeField] RectTransform sizeRect;
    [SerializeField] Image backRarityImage;
    [SerializeField] TextMeshProUGUI cardNameText;
    [SerializeField] TextMeshProUGUI cardDescText;
    [SerializeField] TextMeshProUGUI costText;
    Vector2 targetPos = new Vector2(0, 0.0f);
    Vector2 currentPos = new Vector2(0, 0.0f);
    Vector2 targetScale = new Vector2(1.0f, 0.0f);
    Vector2 currentScale = new Vector2(1.0f, 0.0f);
    //float targetPos = 10.0f;
    //float currentPos = 0.0f;
    RectTransform parentRect;
    Canvas canvas;
    Vector2 margin = new Vector2(0.0f, 0.0f);
    Vector2 marginCard = new Vector2(0.0f, 0.0f);

    public void Init(RectTransform rect, Canvas parentCanvas)
    {
        this.parentRect = parentCanvas.GetComponent<RectTransform>();
        margin = parentRect.sizeDelta * 0.5f;
        this.canvas = parentCanvas;
    }
    private void Update()
    {
        if (isPush)
        {
            Vector3 mousePosition = GetMouseCanvasPosition();
            mousePosition.x += -margin.x + marginCard.x;
            mousePosition.y += marginCard.y;
            currentPos = mousePosition;
        }
        else
        {
            if (isSelect)
            {
                targetPos.y = 30.0f;
            }
            else
            {
                targetPos.y = 0.0f;
            }
        }

        currentPos = Vector2.Lerp(currentPos, targetPos, 0.15f);
        sizeRect.anchoredPosition = currentPos;
    }
    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void OnPointerEnter()
    {
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
}
