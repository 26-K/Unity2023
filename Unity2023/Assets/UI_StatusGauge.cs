using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusGauge : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] Slider slider;

    public void Refresh(int currentHP, int maxHP, int shield = 0)
    {
        bool isShowShield = (shield >= 1);
        shieldObj.SetActive(isShowShield);
        if (isShowShield)
        {
            shieldText.text = $"{shield}";
        }
        hpText.text = $"{currentHP}/{maxHP}";
        float hpRate = (float)currentHP / (float)maxHP;
        slider.value = hpRate;
    }
}
