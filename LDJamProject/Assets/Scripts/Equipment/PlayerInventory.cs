using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<Item> InventoryItems = new List<Item>();
    PlayerStats m_PlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateStats(Item addedItem)
    {

    }

    public void AddToInventory(Item itemToAdd)
    {

    }

    /// <summary>
    /// Might have issue
    /// When the player collides with the object and picks it up
    /// it will call the equipment manager
    /// this is so I dont have to make an entire new script to handle picking up items internally 
    /// </summary>
    /// <param name="collision">Object it is colliding with</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If player is colliding with an item on the ground
        if (collision.gameObject.tag  == "Item")
        {
            // They can pick it up
            if (Input.GetKeyDown("E"))
            {
                EquipmentManager.Instance.PickupItem(collision.gameObject);
            }
        }
    }
}
