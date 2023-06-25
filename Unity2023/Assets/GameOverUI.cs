using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Animator anim;
    [SerializeField] ScoreUI score;
    [SerializeField] GameObject titleButton;
    private void Start()
    {
        obj.SetActive(false);
    }
    public void ShowGameOverUI()
    {
        obj.SetActive(true);
        anim.Play("GameOver");
        DOVirtual.DelayedCall(2.0f, () =>
        {
             score.gameObject.SetActive(true);
             score.Init();
             titleButton.SetActive(true);
        });
    }
}
