using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    public bool isEndGame = false;
    [SerializeField] Player pl;
    [SerializeField] UI_Status statusUI;
    [SerializeField] DataBaseSO dataBaseSO;
    [SerializeField] PlayerInfoManager playerInfoManager;
    [SerializeField] CardManager cardManager;
    [SerializeField] FieldManager fieldManager;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] UI_PopUpManager popUpManager;
    [SerializeField] BattleRewardManager battleRewardManager;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] RelicManager relicManager;
    [SerializeField] DungeonEventManager dungeonEventManager;
    [SerializeField] ComboUIManager comboUIManager;
    public DataBaseSO GetDatabase() => dataBaseSO;
    public CardManager GetCardManager() => cardManager;
    public FieldManager GetFieldManager() => fieldManager;
    public PlayerInfoManager GetPlayerInfoManager() => playerInfoManager;
    public EnemyManager GetEnemyManager() => enemyManager;
    public UI_PopUpManager GetUI_PopUpManager() => popUpManager;
    public BattleRewardManager GetBattleRewardManager() => battleRewardManager;
    public GameOverUI GetGameOverUI() => gameOverUI;
    public RelicManager GetRelicManager() => relicManager;
    public DungeonEventManager GetDungeonEventManager() => dungeonEventManager;
    public ComboUIManager GetComboUIManager() => comboUIManager;

    protected override void UnityAwake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        pl.Init();
        playerInfoManager.Init(this);
        cardManager.Init(this);
        GameStart(); //todo ゲームがスタートした時の処理をここで呼んでいる、他の場所で呼ばせたい
        statusUI.Init(this);
    }

    public void GameStart()
    {
        playerInfoManager.GameStartInit();
        MapManager.Ins.GenerateMap();
        ShowMap();
    }

    public void NextFloor()
    {
        playerInfoManager.floor++; 
        ShowMap();
    }
    public void ShowMap()
    {
        MapManager.Ins.ShowMapSelect();
        battleRewardManager.Hide();
    }

    public void BattleStart()
    {
        fieldManager.ResetField();
        UI_Tutorial.Ins.EndMapTutorial();
        UI_Tutorial.Ins.ShowBattleTutorial();
        TurnManager.Ins.BattleStart();
        enemyManager.BattleStart();
    }

    private void Update()
    {
        //// マウスの位置を取得
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = 10; // オブジェクトの奥行き（Z軸）の位置を設定

        //// マウスの位置をワールド座標に変換
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //// オブジェクトを配置する位置を基準にするためのオフセットを設定
        //Vector3 offset = new Vector3(0, 0, 0); // 適切なオフセットを設定してください

        //// オブジェクトを配置する位置を計算
        //Vector3 placementPosition = worldPosition + offset;

        //// オブジェクトを配置する位置に移動させる
        //objectToPlace.transform.position = placementPosition;

        //// 左クリックでオブジェクトを配置する
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Instantiate(objectToPlace, placementPosition, Quaternion.identity);
        //}
    }

}
