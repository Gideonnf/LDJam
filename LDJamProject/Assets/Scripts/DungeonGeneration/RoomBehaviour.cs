using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    //TODO:: have a variable to 'open doors' once room is done
    [Header("Enemies")]
    public List<Transform> m_PossibleEnemyPositions = new List<Transform>();
    public int m_MinNumberEnemies = 1;

    RoomTypes m_RoomType = RoomTypes.NORMAL_ROOM;
    bool m_RoomComplete = false;
    Vector2Int m_RoomGridPos = Vector2Int.zero;

    public void RoomComplete()
    {
        //open room
        m_RoomComplete = true;
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

    }

    public void SetUpNormalRoom()
    {
        //spawn enemies at possible locations
        //'lock' doors
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
}
