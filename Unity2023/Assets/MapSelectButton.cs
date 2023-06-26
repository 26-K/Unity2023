using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectButton : MonoBehaviour
{
    [SerializeField] Image mapSelectImage;
    [SerializeField] Button button;
    MapMassType mapMassType;
    public void Init(MapMassType type)
    {
        mapMassType = type;
    }
    public void Push()
    {
        if (mapMassType == MapMassType.Battle)
        {
            MapManager.Ins.PushBattleButton();
        }
        else if (mapMassType == MapMassType.Rest)
        {
            MapManager.Ins.PushRestButton();
        }
        else if (mapMassType == MapMassType.Event)
        {
            if (Random.Range(0, 2) == 1) //todo イベント
            {
                MapManager.Ins.PushRestButton();
            }
            else
            {
                MapManager.Ins.PushBattleButton();
            }
        }
        else if (mapMassType == MapMassType.Boss)
        {
            MapManager.Ins.PushBossButton();

        }
        Vector2 pos = this.transform.GetComponent<RectTransform>().anchoredPosition;
        //MapManager.Ins.SetMapPos(pos);
        MapManager.Ins.SetMapPos(pos);
    }

    public void Refresh(bool enable)
    {
        button.enabled = enable;
        if (enable)
        {
            mapSelectImage.color = Color.white;
        }
        else
        {
            mapSelectImage.color = Color.gray;
        }
    }
}
