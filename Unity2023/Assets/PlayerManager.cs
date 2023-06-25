using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject model;


    public void PlayDamage()
    {
        anim.Play("Damage");
    }
    public void PlayAttack()
    {
        anim.Play("Attack");
    }
    public void PlayDie()
    {
        anim.Play("Dead");
    }
    public void PlayIdle()
    {
        anim.Play("Idle");
    }

}
