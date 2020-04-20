using UnityEngine;
using System.Collections.Generic;

public class RoomBehaviour : MonoBehaviour
{
    //TODO:: have a variable to 'open doors' once room is done
    [Header("Enemies")]
    public Transform m_PossibleEnemySpawnPosition;
    public Vector2Int m_MinMaxNumberEnemies = new Vector2Int(3, 5);

    [Header("Room data")]
    public GameObject m_3DCollidersParent;
    public GameObject m_Doors;
    public GameObject m_DoorBlocks;
    public Transform m_PlayerSpawnPosition; //if this is startRoom
    public Transform m_CaravanSpawnPosition;

    [Header("RealizationData")]
    public Transform m_CameraPos;

    RoomTypes m_RoomType = RoomTypes.NORMAL_ROOM;
    bool m_RoomComplete = false;
    Vector2Int m_RoomGridPos = Vector2Int.zero;
    List<GameObject> m_EnemiesInRoom = new List<GameObject>();
    bool m_RoomEnemyStarted = false;

    public void Awake()
    {
        m_RoomComplete = false;
        m_RoomEnemyStarted = false;
        OpenBlocks(false);
    }

    public void Update()
    {
        if (m_RoomComplete)
            return;

        //check if enemies are still alive
        if ((m_RoomType == RoomTypes.NORMAL_ROOM || m_RoomType == RoomTypes.BOSS_ROOM) && m_RoomEnemyStarted)
        {
            foreach(GameObject enemy in m_EnemiesInRoom)
            {
                if (enemy.activeSelf) //if one enemy is active dont bother
                    return;
            }

            if (m_RoomType == RoomTypes.BOSS_ROOM)
                BGMController.Instance.ChangeMusic("NonCombatBGM");

            SoundManager.Instance.Play("RoomClear");
            RoomComplete(); //no more enemies alive, that means room completed
        }
    }

    public void RoomComplete()
    {
        //open room
        m_RoomComplete = true;

        OpenBlocks(false);
        OpenDoors(true);

        //spawn wizard once its done
        if (m_RoomType == RoomTypes.BOSS_ROOM)
        {
            GameObject wizard = DungeonGeneration.Instance.m_Wizard;
            if (wizard != null)
            {
                wizard.SetActive(true);
                wizard.transform.position = m_PossibleEnemySpawnPosition.GetChild(0).position;
            }
        }
    }

    public void OpenDoors(bool open)
    {
        if (m_Doors != null)
            m_Doors.SetActive(open);
    }

    public void OpenBlocks(bool open)
    {
        if (m_DoorBlocks != null)
            m_DoorBlocks.SetActive(open);
    }

    public void SetupRoom()
    {
        //if room is already completed, ignore this
        if (m_RoomComplete)
        {
            OpenDoors(true);
            return;
        }

        switch (m_RoomType)
        {
            case RoomTypes.START_ROOM:
                SetUpStartRoom();
                break;
            case RoomTypes.NORMAL_ROOM:
                //RoomComplete();
                SetUpNormalRoom();
                break;
            case RoomTypes.BOSS_ROOM:
                SetUpBossRoom();
                break;
            case RoomTypes.TRADE_ROOM:
                SetUpTradeRoom();
                break;
        }
    }

    public void SetUpStartRoom()
    {
        //just spawn player in middle
        PlayerController.Instance.ChangePlayerGridPosition(m_RoomGridPos, false);

        //teleport player
        PlayerController.Instance.gameObject.transform.position = m_PlayerSpawnPosition.position;

        //teleport camera pos
        Camera camera = Camera.main;
        if (camera != null)
            camera.transform.position = m_CameraPos.position;

        CameraMovement.Instance.cameraOriPos = m_CameraPos.position;

        //spawn the caravan at the proper place
        GameObject caravan = GameObject.FindGameObjectWithTag("Wagon");
        if (caravan != null)
        {
            if (m_CaravanSpawnPosition != null)
                caravan.transform.position = m_CaravanSpawnPosition.position;
        }

        RoomComplete();
    }

    public void SetUpTradeRoom()
    {
        RoomComplete();
    }

    public void SetUpNormalRoom()
    {
        //spawn enemies at possible locations
        m_EnemiesInRoom.Clear();
        SpawnEnemies();
        m_RoomEnemyStarted = true;

        //blocks activate
        OpenBlocks(true);
        //'lock' doors
        OpenDoors(false);
    }

    public void SpawnEnemies()
    {
        //randomize the number of enemies
        //get random positions
        //spawn enemies there
        if (m_PossibleEnemySpawnPosition == null)
            return;

        int numberOfEnemiesToSpawn = Random.Range(m_MinMaxNumberEnemies.x, m_MinMaxNumberEnemies.y);

        for (int i =0; i < numberOfEnemiesToSpawn; ++i)
        {
            EnemyManager.EnemyType enemyType = (EnemyManager.EnemyType)Random.Range((int)EnemyManager.EnemyType.MELEE_A, (int)EnemyManager.EnemyType.RANGED_A + 1);
            GameObject enemy = EnemyManager.Instance.FetchEnemy(enemyType);

            if (enemy != null)
            {
                int randomLocationIndex = Random.Range(0, m_PossibleEnemySpawnPosition.childCount);
                Vector3 pos = m_PossibleEnemySpawnPosition.GetChild(randomLocationIndex).position;
                enemy.transform.position = pos;
                enemy.SetActive(true);

                EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
                if (enemyBase)
                {
                    enemyBase.Init();
                    enemyBase.Warp(pos); //spawn at a random location
                    m_EnemiesInRoom.Add(enemy);
                }
            }
        }
    }

    public void LeaveRoom()
    {
        OpenDoors(false);
    }

    public void SetUpBossRoom()
    {
        if (m_PossibleEnemySpawnPosition == null)
            return;

        BGMController.Instance.ChangeMusic("CombatBGM");

        //SPAWN BOSS
        Vector3 pos = m_PossibleEnemySpawnPosition.GetChild(0).position;
        GameObject enemy = EnemyManager.Instance.FetchEnemy(EnemyManager.EnemyType.BOSS_A);
        if (enemy != null)
        {
            enemy.transform.position = pos;
            enemy.SetActive(true);
            m_EnemiesInRoom.Add(enemy);

            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase)
            {
                enemyBase.Init();
                enemyBase.Warp(pos); //spawn at a random location
            }
        }
        else //if for some reason no boss
        {
            RoomComplete();
            Debug.Log("WTF");
        }

        m_RoomEnemyStarted = true;

        OpenBlocks(true);
        OpenDoors(false);
    }

    public void SetRoomType(RoomTypes roomType)
    {
        m_RoomType = roomType;
    }

    public void SetRoomGridPos(Vector2Int gridPos)
    {
        m_RoomGridPos = gridPos;
    }

    public void FinishBaking()
    {
        //remove all the 3D colliders
        if (m_3DCollidersParent == null)
            return;

        foreach(Transform child in m_3DCollidersParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
