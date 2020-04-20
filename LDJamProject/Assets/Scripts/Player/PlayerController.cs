using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBase<PlayerController>
{
    // Made this a singleton so that other scripts can just call player controller
    // to get the differet player components

    [SerializeField] Wagon wagon;
    PlayerMovement m_PlayerMovement;
    PlayerCombat m_PlayerCombat;

    public PlayerStats m_PlayerStats;
    public PlayerInventory m_PlayerInventory;

    Vector2Int m_CurrDungeonRoomGridPos = Vector2Int.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to all the components
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerCombat = GetComponent<PlayerCombat>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_PlayerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !m_PlayerMovement.isPullingCaravan)
        {
            m_PlayerMovement.Dash();
        }
        if (Input.GetMouseButton(0) && !m_PlayerMovement.isPullingCaravan)
        {
            m_PlayerCombat.Melee();
        }
        if (Input.GetMouseButton(1) && !m_PlayerMovement.isPullingCaravan)
        {
            m_PlayerCombat.Ranged();
        }
    }


    /// <summary>
    /// For any interaction with E Key
    /// </summary>
    public void Interact()
    {
        if (wagon.playerNearWagon)
            wagon.Interact();
    }

    /// <summary>
    /// Flipping the sprite since its 2D
    /// </summary>
    public void Flip()
    {

    }

    public void ChangePlayerGridPosition(Vector2Int newPlayerPosition, bool updateDungeon = true)
    {
        if (DungeonGeneration.Instance.RoomExists(newPlayerPosition))
            m_CurrDungeonRoomGridPos = newPlayerPosition;
        else
            return;

        if (updateDungeon)
            DungeonGeneration.Instance.ChangeRoom(m_CurrDungeonRoomGridPos);
    }

    public Vector2Int GetPlayerCurrentGridPos()
    {
        return m_CurrDungeonRoomGridPos;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return m_PlayerMovement;
    }

    public bool IsPullingCaravan()
    {
        if (m_PlayerMovement == null)
            return false;

        return m_PlayerMovement.isPullingCaravan;
    }
}
