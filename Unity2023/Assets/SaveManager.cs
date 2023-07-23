using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UserStatus
{
    public int playCount = 0;
    public int gameClearCount = 0;
    public int clearMaxLevel = 0;
    public int currentLevel = 0;
}

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    public UserStatus status;
    protected override void UnityAwake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        status = ES3.Load("userStatus", new UserStatus());
    }

    public void Save()
    {
        ES3.Save("userStatus", status);
    }

    public void AddClearCount()
    {
        status.gameClearCount++;
        status.clearMaxLevel = Mathf.Max(status.clearMaxLevel, status.currentLevel);
        Save();
    }
}
