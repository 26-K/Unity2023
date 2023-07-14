using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public enum MapMassType
{
    None,
    Battle,
    Rest,
    Event,
    Boss,
}

public class MapMassData
{
    public List<MapMassType> massType = new List<MapMassType>();
    public List<int> connectIndex = new List<int>();
    public int floor;
}

public class MapData
{
    public List<MapMassData> massData = new List<MapMassData>();
}

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    [SerializeField] Light lastBossBattleRight;
    [SerializeField] GameObject sizeObj;
    [SerializeField] RectTransform contentObj;
    [SerializeField] UI_MassData uI_MassData;
    [SerializeField] Transform basePos;
    [SerializeField] List<UI_MassData> uI_MassDatas;
    [SerializeField] ScrollRect rect;
    public MapSelectButton ui_RestButton;
    public MapSelectButton ui_BattleButton;
    public MapSelectButton ui_EventButton;
    public MapSelectButton ui_BossButton;
    public MapData mapData = new MapData();
    bool isMapMoveMode = false;
    int stageCount = 18;
    int cellSize = 3;
    public void GenerateMap()
    {
        mapData.massData.Clear();
        //最初の戦闘
        {
            MapMassData data = new MapMassData();
            data.massType.Add(MapMassType.None);
            data.massType.Add(MapMassType.Battle);
            data.massType.Add(MapMassType.None);
            data.floor = 1;
            mapData.massData.Add(data);
        }
        //道中
        for (int i = 0; i < stageCount; i++)
        {
            MapMassData data = new MapMassData();
            int rand = Random.Range(0, 5);
            if (rand == 1)
            {
                data.massType.Add(MapMassType.None);
                data.massType.Add(GetRandomMassType());
                data.massType.Add(MapMassType.None);

            }
            else if (rand == 2)
            {
                data.massType.Add(GetRandomMassType());
                data.massType.Add(GetRandomMassType());
                data.massType.Add(GetRandomMassType());

            }
            else
            {
                data.massType.Add(GetRandomMassType());
                data.massType.Add(MapMassType.None);
                data.massType.Add(GetRandomMassType());
            }
            data.floor = i + 2;
            mapData.massData.Add(data);
        }
        //ボス
        for (int s = 0; s < 1; s++)
        {
            MapMassData data = new MapMassData();
            data.massType.Add(MapMassType.None);
            data.massType.Add(MapMassType.Boss);
            data.massType.Add(MapMassType.None);
            data.floor = 20;
            mapData.massData.Add(data);
        }

        int index = 1;
        Vector3 basePoses = basePos.position;
        foreach (var a in mapData.massData)
        {
            var b = Instantiate(uI_MassData, contentObj.transform);
            var c = new Vector2(basePoses.x, index * 120 + basePoses.y);
            b.transform.position = c;
            b.Init(a, index);
            index++;
            uI_MassDatas.Add(b);
            b.Refresh(InGameManager.Ins.GetPlayerInfoManager().floor);
        }
    }

    public void ShowMapSelect()
    {
        isMapMoveMode = true;
        sizeObj.SetActive(true);
        foreach (var a in uI_MassDatas)
        {
            a.Refresh(InGameManager.Ins.GetPlayerInfoManager().floor);
        }
    }

    public void PushBattleButton()
    {
        if (isMapMoveMode == false)
        {
            return;
        }
        isMapMoveMode = false;
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {
            sizeObj.SetActive(false);
        }).OnComplete(() =>
        {
            InGameManager.Ins.BattleStart();
        }
        );

        UI_Tutorial.Ins.EndMapTutorial();
    }
    public void PushBossButton()
    {
        if (isMapMoveMode == false)
        {
            return;
        }
        isMapMoveMode = false;
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {
            sizeObj.SetActive(false);
        }).OnComplete(() =>
        {
            InGameManager.Ins.BattleStart();
        }
        );
        BGMManager.Ins.PlayFinalBossBGM();
        UI_Tutorial.Ins.EndMapTutorial();
    }
    public void PushRestButton()
    {
        if (isMapMoveMode == false)
        {
            return;
        }
        isMapMoveMode = false;
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {

        }).OnComplete(() =>
        {
            int val = InGameManager.Ins.GetPlayerInfoManager().AddRatioHeal(0.4f);
            CurrentTurnManagerUI.Ins.ShowOtherAnim($"回復ゾーンでHPを{val}回復!");
            InGameManager.Ins.NextFloor();
        }
        );
    }

    public void PushEventButton()
    {
        if (isMapMoveMode == false)
        {
            return;
        }
        isMapMoveMode = false;
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {

        }).OnComplete(() =>
        {
            InGameManager.Ins.GetDungeonEventManager().Show();
        }
        );
    }

    public void SetMapPos(Vector3 pos)
    {
        var targetrect = rect.GetComponent<RectTransform>();
        pos.y = contentObj.rect.height - InGameManager.Ins.GetPlayerInfoManager().floor * 120 - 650;
        pos.x = 0;
        contentObj.anchoredPosition = pos;
    }

    public MapMassType GetRandomMassType()
    {
        var a = Random.Range(0, 8);
        if (a == 1)
        {
            return MapMassType.Rest;

        }
        else if (a == 2 || a == 3)
        {
            return MapMassType.Event;

        }
        else
        {
            return MapMassType.Battle;
        }
    }

    protected override void UnityAwake()
    {
    }
}

/// <summary>
/// ScrollRectの拡張機能を提供します
/// </summary>
public static class ScrollRectExtension
{
    /// <summary>
    /// ScrollRectの上端にGameObjectをあわせる
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="go"></param>
    public static float ScrollToBeBottom(this ScrollRect scrollRect, GameObject go)
    {
        return ScrollToCore(scrollRect, go, 0f);
    }

    /// <summary>
    /// ScrollRectの下端にGameObjectをあわせる
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="go"></param>
    public static float ScrollToBeTop(this ScrollRect scrollRect, GameObject go)
    {
        return ScrollToCore(scrollRect, go, 1f);
    }

    /// <summary>
    /// ScrollRectの縦中央にGameObjectをあわせる
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="go"></param>
    public static float ScrollToCentering(this ScrollRect scrollRect, GameObject go)
    {
        return ScrollToCore(scrollRect, go, 0.5f);
    }

    /// <summary>
    /// ScrollRectのスクロール位置をGameObjectにあわせる
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="go"></param>
    /// <param name="align">0:下、0.5:中央、1:上</param>
    /// <returns></returns>
    static private float ScrollToCore(ScrollRect scrollRect, GameObject go, float align)
    {
        var targetRect = go.transform.GetComponent<RectTransform>();
        var contentHeight = scrollRect.content.rect.height;
        var viewportHeight = scrollRect.viewport.rect.height;
        // スクロール不要
        if (contentHeight < viewportHeight) return 0f;

        // ローカル座標が contentHeight の上辺を0として負の値で格納されてる
        // これは現在のレイアウト特有なのかもしれないので、要確認
        var targetPos = contentHeight + GetPosY(targetRect) + targetRect.rect.height * align;
        var gap = viewportHeight * align; // 上端〜下端あわせのための調整量
        var normalizedPos = (targetPos - gap) / (contentHeight - viewportHeight);

        normalizedPos = Mathf.Clamp01(normalizedPos);
        scrollRect.verticalNormalizedPosition = normalizedPos;
        return normalizedPos;
    }

    static private float GetPosY(RectTransform transform)
    {
        return transform.localPosition.y + transform.rect.y; //pivotによるズレをrect.yで補正
    }
}