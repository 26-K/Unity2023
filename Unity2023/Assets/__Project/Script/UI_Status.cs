using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    InGameManager parent;
    [SerializeField] TextMeshProUGUI scoreText;
    public void Init(InGameManager parent)
    {
        this.parent = parent;
    }

    private void Update()
    {
    }
}
