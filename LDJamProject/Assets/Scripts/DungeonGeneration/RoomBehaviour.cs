using UnityEngine;

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

    [Header("RealizationData")]
    public Transform m_CameraPos;

    RoomTypes m_RoomType = RoomTypes.NORMAL_ROOM;
    bool m_RoomComplete = false;
    Vector2Int m_RoomGridPos = Vector2Int.zero;

    public void Awake()
    {
        m_RoomComplete = false;
        OpenBlocks(false);
    }

    public void RoomComplete()
    {
        //open room
        m_RoomComplete = true;

        OpenBlocks(false);
        OpenDoors(true);
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
                SetUpNormalRoom();
                break;
            case RoomTypes.BOSS_ROOM:
                SetUpBossRoom();
                break;
        }
    }

    public void SetUpStartRoom()
    {
        //just spawn player in middle
        PlayerController.Instance.ChangePlayerGridPosition(m_RoomGridPos, false);

        //teleport player
        PlayerController.Instance.gameObject.transform.position = m_PlayerSpawnPosition.position;

        Debug.Log(PlayerController.Instance.gameObject.transform.position + "   " + m_PlayerSpawnPosition.position);

        //teleport camera pos
        Camera camera = Camera.main;
        if (camera != null)
            camera.transform.position = m_CameraPos.position;

        RoomComplete();
    }

    public void SetUpNormalRoom()
    {
        //spawn enemies at possible locations
        SpawnEnemies();

        //'lock' doors
        OpenDoors(true);
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

                enemy.SetActive(true);
                EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
                if (enemyBase)
                {
                    enemyBase.Init();
                    enemyBase.Warp(m_PossibleEnemySpawnPosition.GetChild(randomLocationIndex).position); //spawn at a random location
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
        //spawn boss
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
