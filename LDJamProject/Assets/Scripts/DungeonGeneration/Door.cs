using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorDirection m_DoorDirection;
    bool m_Entered = false;

    private void OnEnable()
    {
        m_Entered = false;
    }

    private void OnDisable()
    {
        m_Entered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayer(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (DungeonGeneration.Instance.m_LeaveRoomText != null)
                DungeonGeneration.Instance.m_LeaveRoomText.SetActive(false);
        }
    }

    public void CheckPlayer(Collider2D collision)
    {
        if (m_Entered)
            return;

        if (collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                PlayerMovement playerMovement = player.GetPlayerMovement();
                if (playerMovement != null)
                {
                    Vector2Int roomOffset = GetNextRoomOffset();

                    //check if its same direction
                    if (Vector2.Dot(playerMovement.movementDir, roomOffset) > 0)
                    {
                        if (PlayerController.Instance.IsPullingCaravan())
                        {
                            player.ChangePlayerGridPosition(player.GetPlayerCurrentGridPos() + roomOffset);
                            m_Entered = true;
                        }
                        else
                        {
                            //show the is not pulling caravan UI
                            if (DungeonGeneration.Instance.m_LeaveRoomText != null)
                                DungeonGeneration.Instance.m_LeaveRoomText.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    Vector2Int GetNextRoomOffset()
    {
        Vector2Int offset = Vector2Int.zero;

        switch (m_DoorDirection)
        {
            case DoorDirection.TO_LEFT:
                offset = new Vector2Int(-1,0);
                break;
            case DoorDirection.TO_RIGHT:
                offset = new Vector2Int(1, 0);
                break;
            case DoorDirection.TO_UP:
                offset = new Vector2Int(0, 1);
                break;
            case DoorDirection.TO_DOWN:
                offset = new Vector2Int(0, -1);
                break;
        }

        return offset;
    }
}

public enum DoorDirection
{
    TO_LEFT,
    TO_RIGHT,
    TO_UP,
    TO_DOWN
}