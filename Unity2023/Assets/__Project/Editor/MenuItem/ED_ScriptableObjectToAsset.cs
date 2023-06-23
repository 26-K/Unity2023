using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class ED_ScriptableObjectToAsset
{
    readonly static string[] labels = { "Data", "ScriptableObject", string.Empty };

    [MenuItem("Assets/Create/ScriptableObject", priority = 1)]
    static void Create()
    {
        foreach (var selectedObject in Selection.objects)
        {
            // create instance
            var obj = ScriptableObject.CreateInstance(selectedObject.name);
            var n = FileUtil.GetFileName(selectedObject);

            // get path
            var path = getSavePath(selectedObject, n);
            AssetDatabase.CreateAsset(obj, path);
            labels[2] = n;
            // add label
            var sobj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            AssetDatabase.SetLabels(sobj, labels);
            EditorUtility.SetDirty(sobj);
            EditorGUIUtility.PingObject(sobj);
        }
    }

    public static T Create<T>(string className, string file, string name)
    where T : ScriptableObject
    {
        var path = $"{file}{name}.asset";

        if (File.Exists(path))
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        var obj = ScriptableObject.CreateInstance(className);
        AssetDatabase.CreateAsset(obj, path);

        labels[2] = name;
        // add label
        var sobj = AssetDatabase.LoadAssetAtPath<T>(path);
        AssetDatabase.SetLabels(sobj, labels);
        EditorUtility.SetDirty(sobj);
        EditorGUIUtility.PingObject(sobj);

        return sobj;
    }

    static string getSavePath(UnityEngine.Object selectedObject, string name)
    {
        var dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject));
        var SOPath = dirPath.Substring(0, dirPath.LastIndexOf('\\') + 1) + "SO";
        var path = string.Format("{0}/{1}.asset", Directory.Exists(SOPath) ? SOPath : dirPath, name);

        if (File.Exists(path))
            for (var i = 1; ; i++)
            {
                path = string.Format("{0}/{1} ({2}).asset", dirPath, name, i);
                if (!File.Exists(path))
                    break;
            }

        return path;
    }

    // public static string GetFileName(UnityEngine.Object selectedObject) => GetFileName(selectedObject.name);
    // public static string GetFileName(string n) => Oc.StringExtensions.GetFileName(n);
}

public class FileUtil
{
    public static string GetFileName(UnityEngine.Object selectedObject)
     => GetFileName(selectedObject.name);

    public static string GetFileName(Type t)
    {
        var n = t.ToString();
        return GetFileName(n.Substring(n.LastIndexOf(".") + 1));
    }

    public static string GetFileName(string n)
    {
        int i;
        i = n.LastIndexOf("Source");
        if (i > 0)
        {
            n = n.Substring(0, i);
        }
        i = n.LastIndexOf("SO");
        if (i > 0)
        {
            n = n.Substring(0, i);
        }

        if (n.EndsWith("_"))
        {
            n = n.Substring(0, n.Length - 1);
        }

        return $"so--{n}";
    }
}
