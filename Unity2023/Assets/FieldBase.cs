using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBase : MonoBehaviour
{
    [SerializeField] List<SetObjectBase> objectBases = new List<SetObjectBase>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// レリックの効果で大きくする
    /// </summary>
    /// <param name="objectType"></param>
    public void ScaleAllObject(ObjectType objectType)
    {
        foreach (var a in objectBases)
        {
            if (objectType != a.GetObjectType())
            {
                continue;
            }
            Vector3 vec = Vector3.one * 1.225f;

            a.transform.localScale = (a.transform.localScale + vec);
        }
    }
}
