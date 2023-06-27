using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tutorial : SingletonMonoBehaviour<UI_Tutorial>
{
    public GameObject mapTutorial;
    bool mapTutorialEnd = false;
    public GameObject battleTutorial;
    bool battleTutorialEnd = false;
    public GameObject battleTutorial_2;
    bool battleTutorial2_End = false;

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
    public void ShowBattleTutorial_2()
    {
        if (battleTutorial2_End)
        {
            return;
        }
        battleTutorial_2.SetActive(true);
    }

    public void EndBattleTutorial_2()
    {
        battleTutorial2_End = true;
        battleTutorial_2.SetActive(false);
    }
}
