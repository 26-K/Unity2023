using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardBase : MonoBehaviour
{
    public bool isPush;
    public bool isSelect;
    [SerializeField] RectTransform sizePos;
    float targetPos = 10.0f;
    float currentPos = 0.0f;
    private void Update()
    {
        currentPos += isSelect ? 0.5f : -0.5f;
        currentPos = Mathf.Clamp(currentPos, 0.0f, targetPos);
        Vector2 pos = sizePos.localPosition;
        pos.y = currentPos;
        sizePos.localPosition = pos;
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
}
