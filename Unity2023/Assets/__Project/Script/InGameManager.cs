using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    [SerializeField] Player pl;
    [SerializeField] UI_Status statusUI;
    [SerializeField] DataBaseSO dataBaseSO;

    protected override void UnityAwake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        pl.Init();
    }
}
