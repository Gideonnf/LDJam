using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorDirection m_DoorDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ChangePlayerGridPosition(player.GetPlayerCurrentGridPos() + GetNextRoomOffset());
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