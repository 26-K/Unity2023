using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RelicIcon : MonoBehaviour
{
    [SerializeField] Image relicImage;
    [SerializeField] Image subRelicImage;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] RelicBase relicBase;
    [SerializeField] Animator anim;
    [SerializeField] GameObject relicDescObj;
    [SerializeField] TextMeshProUGUI relicNameText;
    [SerializeField] TextMeshProUGUI relicDescText;
    public void Init(RelicBase relicBase)
    {
        this.relicBase = relicBase;
        relicImage.sprite = relicBase.imageSprite;
        subRelicImage.sprite = relicBase.imageSprite;

        countText.gameObject.SetActive(relicBase.countRelic);

    }

    public void RefreshCount()
    {
        if (relicBase.countRelic)
        {
            countText.text = $"{relicBase.relicParam.count}";
        }
        if (relicBase.relicParam.isPlayEnableAnim)
        {
            anim.Play("RelicActiveAnim");
        }
    }

    public void ShowRelicDesc()
    {
        relicNameText.text = $"{relicBase.relicName}";
        relicDescText.text = $"{relicBase.relicDesc}";
        relicDescObj.SetActive(true);
    }

    public void HideRelicDesc()
    {
        relicDescObj.SetActive(false);
    }
}
