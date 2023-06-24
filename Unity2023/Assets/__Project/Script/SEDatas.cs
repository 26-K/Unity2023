using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectHitType
{
    Nail, //釘
    Wall, //壁
}

public class SEDatas : ScriptableObject
{
    public AudioClip cardHover;
    public AudioClip drawCard;
    public AudioClip mapOpen;
    public AudioClip nailHit;
    public AudioClip wallHit;
}
