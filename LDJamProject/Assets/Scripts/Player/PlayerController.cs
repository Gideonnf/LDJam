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
        if (Input.GetKeyDown(KeyCode.Space) && !wagon.playerPullingWagon)
        {
            m_PlayerMovement.Dash();
        }
        if (Input.GetMouseButton(0) && !wagon.playerPullingWagon)
        {
            m_PlayerCombat.Melee();
        }
        if (Input.GetMouseButton(1) && !wagon.playerPullingWagon)
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

}
