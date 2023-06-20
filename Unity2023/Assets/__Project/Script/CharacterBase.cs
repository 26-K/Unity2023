using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [LabelText("HP")]public int HP = 0;
    [LabelText("最大HP")]public int MaxHP = 0;
    public int id = 0;
    public SideType side;
}

public enum SideType
{
    Side_Player,
    Side_Enemy,
}

public class AbilityRetention
{
}