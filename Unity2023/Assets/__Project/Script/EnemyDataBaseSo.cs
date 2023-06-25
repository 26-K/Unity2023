using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public int minFloor = 0;
    public int maxFloor = 10;
    public EnemyBase enemy;
}

public class EnemyDataBaseSo : ScriptableObject
{
    [SerializeField]
    [LabelText("敵データ")] public List<EnemyData> enemyDatas = new List<EnemyData>();
}
