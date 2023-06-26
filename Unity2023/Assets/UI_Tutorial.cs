using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tutorial : SingletonMonoBehaviour<UI_Tutorial>
{
    public GameObject mapTutorial;
    bool mapTutorialEnd = false;
    public GameObject battleTutorial;
    bool battleTutorialEnd = false;

    protected override void UnityAwake()
    {
        ShowMapTutorial();
    }

    public void ShowMapTutorial()
    {
        if (mapTutorialEnd)
        {
            return;
        }
        mapTutorial.SetActive(true);
    }

    public void EndMapTutorial()
    {
        mapTutorialEnd = true;
        mapTutorial.SetActive(false);
    }
    public void ShowBattleTutorial()
    {
        if (battleTutorialEnd)
        {
            return;
        }
        battleTutorial.SetActive(true);
    }

    public void EndBattleTutorial()
    {
        battleTutorialEnd = true;
        battleTutorial.SetActive(false);
    }
}
