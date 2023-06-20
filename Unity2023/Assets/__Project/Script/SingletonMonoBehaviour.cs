using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : PPD_MonoBehaviour
where T : SingletonMonoBehaviour<T>
{
    public static T Ins { get; private set; }

    protected abstract void UnityAwake();

    private void Awake()
    {
        if (enabled == false)
        {
            return;
        }
        if (Ins == null)
        {
            //ゲーム開始時にGameManagerをinstanceに指定ß
            Ins = this as T;
            UnityAwake();
        }
        else if (Ins != this)
        {
            Debug.LogError("SingletonFail");
        }
        else
        {
            // Do Nothing
        }
    }

    protected virtual void OnDestroy()
    {
        Ins = null;
    }

#if UNITY_EDITOR
    public static T InsEditor()
    {
        if (Ins != null)
        {
            return Ins;
        }
        else
        {
            Ins = GameObject.FindObjectOfType<T>();
            return Ins;
        }
    }
#endif
}

