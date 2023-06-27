using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyBase> enemys = new List<EnemyBase>();
    [SerializeField] Canvas subCanvas;
    [SerializeField] Camera subCamera;
    [SerializeField] GameObject damageEffectFire;
    [SerializeField] UI_SpeechBubble speechBubble;
    [SerializeField] UI_StatusGauge statusGauge;
    [SerializeField] EnemtAttackEntryUI enemyAttackEntryUI;

    float waitTimer = 0.0f;

    public void Start()
    {
        Refresh();
    }

    public bool IsDestroyAllEnemy()
    {
        foreach (var a in enemys)
        {
            if (a.GetNowHP() > 0)
            {
                return false;
            }
        }
        return true;
    }
    public void BattleStart()
    {
        foreach (var a in enemys)
        {
            Destroy(a.gameObject);
        }
        enemys.Clear();
        var b = Instantiate(InGameManager.Ins.GetDatabase().GetRandomEnemy(InGameManager.Ins.GetPlayerInfoManager().floor), this.transform);
        enemys.Add(b);
        foreach (var a in enemys)
        {
            a.Init();
            enemyAttackEntryUI.Init(a.GetEnemyActions());
        }
        Refresh();
    }
    public void AddDamage(int damageVal)
    {
        var a = GetRandom(enemys);
        if (a != null)
        {
            InGameManager.Ins.GetPlayerInfoManager().player.PlayAttack();
            AudioManager.Ins.PlayFireImpactSound();
            InGameManager.Ins.GetPlayerInfoManager().SetDiffDamage(damageVal);
            a.ObtainDamage(damageVal);
            Instantiate(damageEffectFire).transform.position = a.transform.position;
            InGameManager.Ins.GetUI_PopUpManager().ShowPopUpTextSub(a.transform, subCamera, $"{damageVal}", "DamagePopUp");
            Refresh();
        }
    }

    T GetRandom<T>(List<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
    public void Update()
    {
        if (TurnManager.Ins.GetCurrentTurn == TurnState.EnemyTurn)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= 0.7f)
            {
                DoEnemyAction();
                waitTimer = 0.0f;
                return;
            }
        }
    }

    public void DoEnemyAction()
    {
        foreach (var a in enemys)
        {
            if (a.GetIsActionEnd() == false)
            {
                a.DoEnemyAction();
                return;
            }
        }
        TurnManager.Ins.StartPlayerTurn();
    }

    public void TurnProgression()
    {
        foreach (var a in enemys)
        {
            a.TurnProgression();
            enemyAttackEntryUI.Init(a.GetEnemyActions());
        }
    }

    /// <summary>
    /// セリフを喋らせる
    /// </summary>
    /// <param name="en"></param>
    /// <param name="str"></param>
    public void DoQuote(EnemyBase en, string str)
    {
        var a = Instantiate(speechBubble, subCanvas.transform);
        Vector3 pos = en.transform.position;
        a.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(subCamera, pos) + Vector2.up * 200;
        a.Init(str);
    }

    public void Refresh()
    {
        if (enemys.Count <= 0)
        {
            statusGauge.gameObject.SetActive(false);
        }
        int liveEnemy = 0;
        foreach (var a in enemys)
        {
            liveEnemy += a.GetNowHP() >= 1 ? 1 : 0;
            statusGauge.Refresh(a.GetNowHP(), a.GetBaseHP(),a.GetShield());
        }
        statusGauge.gameObject.SetActive(liveEnemy > 0);
    }
}
