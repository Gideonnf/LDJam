using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBlock : MonoBehaviour
{
    public float m_MaxRadius = 1.0f;
    public float m_Force = 1.0f;
    public PushDirection m_PushDirection;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, m_MaxRadius);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //get the distance between the starting point and where the player is
            float magnitude = Vector2.Distance(collision.gameObject.transform.position, transform.position);
            Vector2 dir = GetPushDiretion();

            collision.gameObject.transform.position += (Vector3)(dir * Mathf.Abs(((m_MaxRadius) - magnitude) * m_Force));
        }
    }

    Vector2Int GetPushDiretion()
    {
        Vector2Int offset = Vector2Int.zero;

        switch (m_PushDirection)
        {
            case PushDirection.TO_LEFT:
                offset = new Vector2Int(-1, 0);
                break;
            case PushDirection.TO_RIGHT:
                offset = new Vector2Int(1, 0);
                break;
            case PushDirection.TO_UP:
                offset = new Vector2Int(0, 1);
                break;
            case PushDirection.TO_DOWN:
                offset = new Vector2Int(0, -1);
                break;
        }

        return offset;
    }
}

public enum PushDirection
{
    TO_LEFT,
    TO_RIGHT,
    TO_UP,
    TO_DOWN
}