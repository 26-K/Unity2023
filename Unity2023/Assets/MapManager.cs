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
}

public class MapData
{
    public List<MapMassData> massData = new List<MapMassData>();
}

public class MapManager : MonoBehaviour
{
    [SerializeField] 
    public MapData mapData = new MapData();
    int stageCount = 20;
    int cellSize = 3;
    public void GenerateMap()
    {
        mapData.massData.Clear();
        //最初の戦闘
        for (int s = 0; s < cellSize; s++)
        {
            MapMassData data = new MapMassData();
            data.massType.Add(MapMassType.None);
            data.massType.Add(MapMassType.Battle);
            data.massType.Add(MapMassType.None);
            mapData.massData.Add(data);
        }
        //道中
        for (int i = 0; i < stageCount; i++)
        {
            MapMassData data = new MapMassData();
            int rand = Random.Range(0, 5);
            if (rand == 1)
            {
                for (int s = 0; s < cellSize; s++)
                {
                    data.massType.Add(MapMassType.None);
                    data.massType.Add(MapMassType.Battle);
                    data.massType.Add(MapMassType.None);
                }
            }
            else if (rand == 2)
            {
                for (int s = 0; s < cellSize; s++)
                {
                    data.massType.Add(MapMassType.Battle);
                    data.massType.Add(MapMassType.Battle);
                    data.massType.Add(MapMassType.Battle);
                }
            }
            else
            {
                for (int s = 0; s < cellSize; s++)
                {
                    data.massType.Add(MapMassType.Battle);
                    data.massType.Add(MapMassType.None);
                    data.massType.Add(MapMassType.Battle);
                }
            }
            mapData.massData.Add(data);
        }
        //ボス
        for (int s = 0; s < cellSize; s++)
        {
            MapMassData data = new MapMassData();
            data.massType.Add(MapMassType.Boss);
            data.massType.Add(MapMassType.Boss);
            data.massType.Add(MapMassType.Boss);
            mapData.massData.Add(data);
        }
    }
}
