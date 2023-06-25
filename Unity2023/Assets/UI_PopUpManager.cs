using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUpManager : MonoBehaviour
{
    [SerializeField] UI_PopUp popUp;
    [SerializeField] Canvas parent;
    [SerializeField] Canvas subCanvas;
    [SerializeField] Camera subCamera;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="en"></param>
    /// <param name="str"></param>
    public void ShowPopUpText(Transform en, string str,string anim = "")
    {
        var a = Instantiate(popUp, this.transform);
        Vector3 pos = en.transform.position;
        a.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        a.Init(str,anim);
    }
    public void ShowPopUpTextSub(Transform en, Camera cam, string str, string anim = "")
    {
        var a = Instantiate(popUp, this.transform);
        Vector3 pos = en.transform.position;
        Vector2 random = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
        a.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(cam, pos) + random;
        a.Init(str, anim);
    }
    public void ShowPopUpTextSub(Transform en, string str, string anim = "")
    {
        var a = Instantiate(popUp, this.transform);
        Vector3 pos = en.transform.position;
        Vector2 random = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
        a.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(subCamera, pos) + random;
        a.Init(str, anim);
    }
}
