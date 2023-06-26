using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] GameObject sizeObj;
    [SerializeField] GameObject contentObj;
    [SerializeField] UI_MassData uI_MassData;
    [SerializeField] Transform basePos;
    [SerializeField] List<UI_MassData> uI_MassDatas;
    public MapSelectButton ui_RestButton;
    public MapSelectButton ui_BattleButton;
    public MapSelectButton ui_EventButton;
    public MapSelectButton ui_BossButton;
    public MapData mapData = new MapData();
    bool isMapMoveMode = false;
    int stageCount = 11;
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
            data.floor = 13;
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
    public void PushRestButton()
    {
        isMapMoveMode = false;
        UI_SceneChangeAnim.Ins.PlayAnim();
        DOVirtual.DelayedCall(0.4f, () =>
        {

        }).OnComplete(() =>
        {
            int val = InGameManager.Ins.GetPlayerInfoManager().AddRatioHeal(0.3f);
            CurrentTurnManagerUI.Ins.ShowOtherAnim($"回復ゾーンでHPを{val}回復!");
            InGameManager.Ins.NextFloor();
        }
        );
    }

    public MapMassType GetRandomMassType()
    {
        var a = Random.Range(0, 5);
        if (a == 1)
        {
            return MapMassType.Rest;

        }
        else if (a == 2)
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
