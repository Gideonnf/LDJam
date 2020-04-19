using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    //TODO:: have a variable to 'open doors' once room is done
    [Header("Enemies")]
    public List<Transform> m_PossibleEnemyPositions = new List<Transform>();
    public int m_MinNumberEnemies = 1;

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

    public void RoomComplete()
    {
        //open room
        m_RoomComplete = true;

        if (m_DoorBlocks != null)
            m_DoorBlocks.SetActive(false);

        if (m_Doors != null)
            m_Doors.SetActive(true);
    }

    public void SetupRoom()
    {
        //if room is already completed, ignore this
        if (m_RoomComplete)
            return;

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

        //teleport camera pos
        Camera camera = Camera.main;
        if (camera != null)
            camera.transform.position = m_CameraPos.position;

        RoomComplete();
    }

    public void SetUpNormalRoom()
    {
        //spawn enemies at possible locations
        //'lock' doors

        //teleport camera pos
        //Camera camera = Camera.main;
        //if (camera != null)
        //    camera.transform.position = m_CameraPos.position;
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
        foreach(Transform child in m_3DCollidersParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
