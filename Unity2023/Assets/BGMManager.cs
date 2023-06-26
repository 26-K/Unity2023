using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource audios;
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
    }
}
