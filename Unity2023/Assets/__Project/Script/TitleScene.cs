using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : SingletonMonoBehaviour<TitleScene>
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject startFlash;
    [SerializeField] Animator anim;
    [SerializeField] Animator fadeInAnim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource seSource;

    [SerializeField] GameObject optionObj;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] TextMeshProUGUI bgmText;
    [SerializeField] TextMeshProUGUI seText;
    // Start is called before the first frame update
    public bool isPlayBGM = true;
    void Start()
    {
        Application.targetFrameRate = 60;
        DOVirtual.DelayedCall(1.0f, () => title.SetActive(true));
        DOVirtual.DelayedCall(1.5f, () => startButton.SetActive(true));
    }

    private void Update()
    {
        if (isPlayBGM == false)
        {
            audioSource.volume -= Time.deltaTime;
        }
    }
    public void PushStartButton()
    {
        AudioManager.Ins.PlayGameStartSound();
        isPlayBGM = false;
        Camera.main.DOShakePosition(0.5f, 10.0f, 30, 30, true);
        //Camera.main.DOShakeRotation(0.3f, 4.0f, 30, 30, true);
        startFlash.SetActive(true);
        DOVirtual.DelayedCall(0.75f, () =>
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

    public void OpenOption()
    {
        optionObj.SetActive(true);
        bgmSlider.value = GlobalSettingManager.bgmRate;
        seSlider.value = GlobalSettingManager.seRate;
        bgmText.text = $"{(int)(bgmSlider.value * 100)}";
        seText.text = $"{(int)(seSlider.value * 100)}";
    }

    public void ChangeBGM(float val)
    {
        GlobalSettingManager.Ins.ChangeBGMVolume(bgmSlider.value);
        bgmText.text = $"{(int)(bgmSlider.value * 100)}";
        seText.text = $"{(int)(seSlider.value * 100)}";
    }
    public void ChangeSE(float val)
    {
        GlobalSettingManager.Ins.ChangeSEVolume(seSlider.value);
        bgmText.text = $"{(int)(bgmSlider.value * 100)}";
        seText.text = $"{(int)(seSlider.value * 100)}";
    }
    public void PlaySE()
    {
        seSource.Play();
    }

    protected override void UnityAwake()
    {
    }
}
