using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AssensionType
{
    AddHP = 1,
    AddAtkRate = 2,
    TurnAtk = 3,
    DecHeal = 4,
    DecAtk = 5,
    DecDef = 6,
    NoDraw = 7,
    AddHP_2 = 8,
    AddAtkRate_2 = 9,
    AddEnemyAct = 10,
}

public class AssensionManager : SingletonMonoBehaviour<AssensionManager>
{
    [SerializeField] GameObject assensionObj;
    [SerializeField] TextMeshProUGUI assensionDesc;
    [SerializeField] TextMeshProUGUI assensionLevelText;
    [SerializeField] int currentAssension;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Ins == null)
        {
            return;
        }
        if (assensionObj != null)
        {
            assensionObj.SetActive((SaveManager.Ins.status.gameClearCount >= 1));
        }

        SetAssension(SaveManager.Ins.status.currentLevel);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.L))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SaveManager.Ins.status.clearMaxLevel = 10;
                    SaveManager.Ins.Save();
                    AudioManager.Ins.PlayDuplicateSound();
                }
            }
        }
    }

    void SetAssension(int lv)
    {
        currentAssension = lv;
        SaveManager.Ins.status.currentLevel = currentAssension;
        SaveManager.Ins.Save();
        if (assensionDesc == null)
        {
            return;
        }
        if (assensionLevelText == null)
        {
            return;
        }
        assensionDesc.text = $"{GetAssensionText(lv)}";
        assensionLevelText.text = $"{lv}";
    }
    public int GetAssension() => SaveManager.Ins.status.currentLevel;

    public void AddAssension()
    {
        currentAssension++;
        if (currentAssension < 0)
        {
            currentAssension = 0;
        }
        if (currentAssension > SaveManager.Ins.status.clearMaxLevel + 1)
        {
            currentAssension = SaveManager.Ins.status.clearMaxLevel + 1;
        }
        if (currentAssension > 10)
        {
            currentAssension = 10;
        }
        SetAssension(currentAssension);
    }
    public void DecAssension()
    {
        currentAssension--;
        if (currentAssension < 0)
        {
            currentAssension = 0;
        }
        if (currentAssension > 10)
        {
            currentAssension = 10;
        }
        SetAssension(currentAssension);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public float GetAddHpRate()
    {
        float rate = 1.0f;
        if (currentAssension >= (int)AssensionType.AddHP)
        {
            rate += 0.15f;
        }
        if (currentAssension >= (int)AssensionType.AddHP_2)
        {
            rate += 0.3f;
        }
        return rate;
    }

    public float GetAddAtkRate()
    {
        float rate = 1.0f;
        if (currentAssension >= (int)AssensionType.AddAtkRate)
        {
            rate += 0.2f;
        }
        if (currentAssension >= (int)AssensionType.AddAtkRate_2)
        {
            rate += 0.2f;
        }
        return rate;
    }

    public int GetAddTurnAtk()
    {
        int addAtk = 0;
        if (currentAssension >= (int)AssensionType.TurnAtk)
        {
            addAtk += 1;
        }
        return addAtk;
    }


    public float GetDecHealRatio()
    {
        float decRate = 0.0f;
        if (currentAssension >= (int)AssensionType.DecHeal)
        {
            decRate += 0.07f;
        }
        return decRate * -1.0f;
    }

    public int GetDecAtk()
    {
        int decAtk = 0;
        if (currentAssension >= (int)AssensionType.DecAtk)
        {
            decAtk += 1;
        }
        return decAtk;
    }
    public int GetDecDef()
    {
        int decDef = 0;
        if (currentAssension >= (int)AssensionType.DecDef)
        {
            decDef += 1;
        }
        return decDef;
    }

    public bool GetIsNoDraw() //自発捨て札を山札に戻すができなくなる
    {
        if (currentAssension >= (int)AssensionType.NoDraw)
        {
            return true;
        }
        return false;
    }
    public int GetSkipEnemyAction()
    {
        int skipCnt = 0;
        if (currentAssension >= (int)AssensionType.AddEnemyAct)
        {
            skipCnt += 1;
        }
        return skipCnt;
    }

    public string GetAssensionText(int lv)
    {
        var x = lv switch
        {
            (int)AssensionType.AddHP => "敵のHPの上昇",
            (int)AssensionType.AddAtkRate =>"敵の攻撃力が上昇",
            (int)AssensionType.TurnAtk => "敵の攻撃力がターン毎にどんどん上がっていく",
            (int)AssensionType.DecHeal => "回復マスでの回復量低下",
            (int)AssensionType.DecAtk  => "アタックゲートの弱体化",
            (int)AssensionType.AddHP_2 => "敵のHPが更に上昇",
            (int)AssensionType.AddAtkRate_2 =>"敵の攻撃力が更に上昇",
            (int)AssensionType.DecDef => "ガードゲートの弱体化",
            (int)AssensionType.NoDraw => "カードの効果のドローで捨て札のカードを山札に戻せなくなる",
            (int)AssensionType.AddEnemyAct => "敵の行動の激化",
            _ => "なし",
        };
        return x;
    }

    

    protected override void UnityAwake()
    {
    }
}
