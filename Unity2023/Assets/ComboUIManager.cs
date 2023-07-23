using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboUIManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator hitAnim;
    [SerializeField] GameObject sizeObj;
    [SerializeField] TextMeshProUGUI maxComboText;
    [SerializeField] TextMeshProUGUI hitText;
    [SerializeField] TextMeshProUGUI totalDamageText;
    [SerializeField] GameObject totalHitObj;
    int maxCombo = 0;
    int totalDamage = 0;
    int hitCount = 0;

    public void Start()
    {
        maxCombo = 0;
        totalDamage = 0;
        hitCount = 0;
        sizeObj.SetActive(false);
    }

    public void Refresh(int damage, int combo = 0)
    {
        sizeObj.SetActive(true);
        anim.Play("FadeIn");
        maxCombo = Mathf.Max(maxCombo,combo);
        totalDamage += damage;
        hitCount += 1;
        totalHitObj.SetActive((hitCount >= 2));
        hitAnim.Play("TotalHit", 0, 0.0f);
        maxComboText.text = $"{maxCombo}Combo";
        hitText.text = $"{hitCount}";
        totalDamageText.text = $"{totalDamage}Damage";
    }

    public void Hide()
    {
        DOVirtual.DelayedCall(3.0f, () =>
         {
            maxCombo = 0;
            totalDamage = 0;
            hitCount = 0;
            anim.Play("FadeOut");
         });
    }
}
