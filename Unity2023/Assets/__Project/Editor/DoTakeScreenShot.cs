using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DoTakeScreenShot : MonoBehaviour
{

    [MenuItem("Tools/Capture")]
    public static void TakeScreenShot()
    {

        string projName = PlayerSettings.productName; //プロジェクト名
        string width = $"{Screen.width}"; //横幅
        string height = $"{Screen.height}"; //縦幅

        string fileBaseName = projName + "_" + width + "x" + height; //ApplicationName_1242x2208 ←といった具合に生成

        string folderpass = $"{Application.dataPath}/../Capture/";
        for (int i = 1; i < 100; i++)
        {
            string serialNumber = "_" + (i).ToString();
            if (!File.Exists(folderpass + fileBaseName + serialNumber + ".png")) //同名のファイルが存在しない場合
            {
                ScreenCapture.CaptureScreenshot(string.Format($"{folderpass + fileBaseName + serialNumber + ".png"}"));
                Debug.Log($"{folderpass + fileBaseName + serialNumber + ".png"}");
                break;
            }
        }
    }
}
