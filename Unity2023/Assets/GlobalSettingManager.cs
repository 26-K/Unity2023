using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingManager : SingletonMonoBehaviour<GlobalSettingManager>
{
    public static float seRate = 0.71f;
    public static float bgmRate = 0.71f;
    public static bool ignoreTutorial = false;
    protected override void UnityAwake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

    }

    public void ChangeBGMVolume(float rate)
    {
        bgmRate = rate;
    }

    public void ChangeSEVolume(float rate)
    {
        seRate = rate;
    }

    public float GetBGMVolume()
    {
        return bgmRate;
    }
    public float GetSEVolume()
    {
        return seRate;
    }
}
