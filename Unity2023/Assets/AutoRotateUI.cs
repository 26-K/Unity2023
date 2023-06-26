using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateUI : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    private void Update()
    {
        Vector3 speed = new Vector3(0, 0, 600);
        rect.localRotation *= Quaternion.Euler(speed * Time.deltaTime);
    }
}
