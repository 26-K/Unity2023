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

        }
        else if (mapMassType == MapMassType.Event)
        {

        }
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
