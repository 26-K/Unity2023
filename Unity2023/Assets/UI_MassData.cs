using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MassData : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    MapMassData massData;
    List<MapSelectButton> mapButtons = new List<MapSelectButton>();
    int floor = 1;

    public void Init(MapMassData massData, int index)
    {
        floor = index;
        this.massData = massData;
        int now = 0;
        foreach (var a in massData.massType)
        {
            if (a == MapMassType.Battle)
            {
                var b = Instantiate(MapManager.Ins.ui_BattleButton, this.transform);
                Vector2 pos = Vector2.zero;
                pos.x = 200 * now - 200;
                pos.y = Random.Range(-10, 10);
                b.transform.localPosition = pos;
                b.Init(a);
                mapButtons.Add(b);
            }
            else if (a == MapMassType.Event)
            {
                var b = Instantiate(MapManager.Ins.ui_EventButton, this.transform);
                b.transform.localPosition = Vector3.zero;
                Vector2 pos = Vector2.zero;
                pos.x = 200 * now - 200;
                pos.y = Random.Range(-10, 10);
                b.transform.localPosition = pos;
                b.Init(a);
                mapButtons.Add(b);
            }
            else if (a == MapMassType.Rest)
            {
                var b = Instantiate(MapManager.Ins.ui_RestButton, this.transform);
                b.transform.localPosition = Vector3.zero;
                Vector2 pos = Vector2.zero;
                pos.x = 200 * now - 200;
                pos.y = Random.Range(-10, 10);
                b.transform.localPosition = pos;
                b.Init(a);
                mapButtons.Add(b);
            }
            else if (a == MapMassType.Boss)
            {
                var b = Instantiate(MapManager.Ins.ui_BossButton, this.transform);
                b.transform.localPosition = Vector3.zero;
                Vector2 pos = Vector2.zero;
                pos.x = 200 * now - 200;
                pos.y = Random.Range(-10, 10);
                b.transform.localPosition = pos;
                b.Init(a);
                mapButtons.Add(b);
            }
            now++;
        }
    }
    public void Refresh(int floor)
    {
        foreach (var item in mapButtons)
        {
            item.Refresh((this.floor == floor));
        }
    }
}
