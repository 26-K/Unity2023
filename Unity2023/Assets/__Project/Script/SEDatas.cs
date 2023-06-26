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
    public AudioClip oneMore;
    public AudioClip gateIn;
    public AudioClip duplicate;
    public AudioClip battleStart;
    public AudioClip turnStart;
    public AudioClip setObject;
    public AudioClip cardSelect;
    public AudioClip fireImpact;
    public AudioClip gameStart;
}
