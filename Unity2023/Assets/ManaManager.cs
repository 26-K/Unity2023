using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI manaText;

    CardManager parent;
    // Start is called before the first frame update
    public void Init(CardManager parent)
    {
        this.parent = parent;
    }

    public void PlayChargeManaAnim()
    {
        anim.Play("ManaChargeAnim");
    }
    public void Refresh()
    {
        int nowMana = parent.mana;
        int maxMana = parent.maxMana;
        manaText.text = $"{nowMana}/{maxMana}";
    }
}
