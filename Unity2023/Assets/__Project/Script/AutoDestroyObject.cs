using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;


public class AutoDestroyObject : MonoBehaviour
{
    [SerializeField, LabelText("自動削除（秒） 0で無効"), Min(0)]
    private float autoDestroy_sec = 1;
    [LabelText("削除じゃなくて非アクティブにするだけ")]
    public bool JustInactive;
    public float AutoDestroySec
    {
        set
        {
            autoDestroy_sec = value;
            this.StartCoroutine(ErDestroy());
        }
    }

    private void Awake()
    {
        if (autoDestroy_sec > 0)
        {
            this.StartCoroutine(ErDestroy());
        }
    }

    IEnumerator ErDestroy()
    {
        yield return new WaitForSeconds(autoDestroy_sec);
        if (JustInactive)
            this.transform.gameObject.SetActive(false);
        else
        {
            Destroy(this.gameObject);
        }
    }

}