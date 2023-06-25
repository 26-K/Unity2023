using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator anim;
    float removeTimer = 1.0f;
    public void Init(string str, string animName = "")
    {
        if (animName != "")
        {
            anim.Play(animName);
        }
        text.text = $"{str}";
        removeTimer = 2.0f;
    }

    public void Update()
    {
        removeTimer -= Time.deltaTime;
        if (removeTimer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
