using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpeechBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    float removeTimer = 5.0f;

    public void Init(string str)
    {
        text.text = str;
        removeTimer = 5.0f;
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
