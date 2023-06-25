using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SceneChangeAnim : SingletonMonoBehaviour<UI_SceneChangeAnim>
{
    [SerializeField] Animator anim;

    protected override void UnityAwake()
    {
        
    }

    public void PlayAnim()
    {
        anim.Play("Enter");
    }
}
