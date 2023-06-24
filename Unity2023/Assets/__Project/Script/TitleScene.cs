using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject startFlash;
    [SerializeField] Animator anim;
    [SerializeField] Animator fadeInAnim;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        DOVirtual.DelayedCall(1.0f, ()=> title.SetActive(true));
        DOVirtual.DelayedCall(1.5f, ()=> startButton.SetActive(true));
    }

    public void PushStartButton()
    {
        Camera.main.DOShakePosition(0.5f, 10.0f, 30, 30, true);
        //Camera.main.DOShakeRotation(0.3f, 4.0f, 30, 30, true);
        startFlash.SetActive(true);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            fadeInAnim.Play("FadeIn");
            Camera.main.DOFieldOfView(50.0f, 1.0f);
        });
        DOVirtual.DelayedCall(1.6f, () =>
        {
            SceneManager.LoadScene("MainInGameScene");
        });
        anim.Play("GameStart");
    }
}
