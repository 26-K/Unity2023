using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールの発射準備場所
/// </summary>
public class LaunchReady : MonoBehaviour
{
    BulletState state = new BulletState(); //発射される弾のスタッツ


    public BulletState GetState()
    {
        return state;
    }

    public void AddPow(int val)
    {
        state.pow += val;
    }
    public void AddCombo(int val)
    {
        state.comboCount += val;
        state.totalComboCount += val;
    }
}
