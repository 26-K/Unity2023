using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscardPileManager : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshProUGUI pileCountText;

    CardManager parent;
    // Update is called once per frame
    private void Start()
    {
        this.rect = this.GetComponent<RectTransform>();
    }

    public void Init(CardManager parent)
    {
        this.parent = parent;
    }
    public void SelfUpdate()
    {
        foreach (var a in parent.discardPile)
        {
            a.SetDiscard(this.rect.localPosition);
        }
        pileCountText.text = $"{parent.discardPile.Count}";
    }
}
