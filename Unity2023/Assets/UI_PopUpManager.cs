using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUpManager : MonoBehaviour
{
    [SerializeField] UI_PopUp popUp;
    [SerializeField] Canvas parent;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="en"></param>
    /// <param name="str"></param>
    public void ShowPopUpText(Transform en, string str)
    {
        var a = Instantiate(popUp, this.transform);
        Vector3 pos = en.transform.position;
        a.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        a.Init(str);
    }
}
