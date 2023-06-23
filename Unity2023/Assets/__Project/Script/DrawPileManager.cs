using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawPileManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI pileCountText;
    [SerializeField] RectTransform rect;

    CardManager parent;
    public void Init(CardManager parent)
    {
        this.parent = parent;
    }

    public void SelfUpdate()
    {
        foreach (var a in parent.deck)
        {
            a.SetDrawPile(this.rect.localPosition);
        }
        pileCountText.text = $"{parent.deck.Count}";
    }

    public RectTransform GetRect()
    {
        return rect;
    }
}
