using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{
    [SerializeField] AudioSource audios;
    [SerializeField] AudioClip boss;
    // Start is called before the first frame update
    void Start()
    {
        audios = this.transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleScene.Ins != null && TitleScene.Ins.isPlayBGM)
        {
            audios.volume = GlobalSettingManager.Ins.GetBGMVolume();
        }
        if (InGameManager.Ins != null && InGameManager.Ins.isEndGame != false)
        {
            audios.volume = audios.volume - 0.01f;
        }
    }

    public void PlayFinalBossBGM()
    {
        audios.clip = boss;
        audios.Play();
    }

    protected override void UnityAwake()
    {
    }
}
