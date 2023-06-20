﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject startFlash;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(1.0f, ()=> title.SetActive(true));
        DOVirtual.DelayedCall(1.5f, ()=> startButton.SetActive(true));
    }

    public void PushStartButton()
    {
        Camera.main.DOShakePosition(0.5f, 10.0f, 30, 30, true);
        //Camera.main.DOShakeRotation(0.3f, 4.0f, 30, 30, true);
        startFlash.SetActive(true);
        DOVirtual.DelayedCall(1.0f, () => Debug.Log("次のシーンへ"));
        anim.Play("GameStart");
    }
}