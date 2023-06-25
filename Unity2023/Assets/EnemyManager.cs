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

    float waitTimer = 0.0f;

    public void Start()
    {
        Refresh();
    }
    public void BattleStart()
    {
        var b = Instantiate(InGameManager.Ins.GetDatabase().GetRandomEnemy(InGameManager.Ins.GetPlayerInfoManager().floor), this.transform);
        enemys.Add(b);
        foreach (var a in enemys)
        {
            a.Init();
        }
        Refresh();
    }
    public void AddDamage(int damageVal)
    {
        var a = GetRandom(enemys);
        if (a != null)
        {
            InGameManager.Ins.GetPlayerInfoManager().player.PlayAttack();
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
            if (waitTimer >= 0.5f)
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
            statusGauge.Refresh(a.GetNowHP(), a.GetBaseHP());
        }
        statusGauge.gameObject.SetActive(liveEnemy > 0);
    }
}
