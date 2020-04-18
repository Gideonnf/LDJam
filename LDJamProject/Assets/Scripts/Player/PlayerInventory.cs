using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public List<ItemObjBase> UniqueItems = new List<ItemObjBase>();
    List<ItemObjBase> InventoryItems = new List<ItemObjBase>();
    PlayerStats m_PlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < UniqueItems.Count; ++i)
        {
            UniqueItems[i].PassiveEffect();
        }
    }


    public void AddStats(GameObject addedItem)
    {
        ItemObjBase ItemBase = addedItem.GetComponent<ItemObjBase>();

        // Generic item stats are applied to player
        // i.e potion only has health gain
        // weapon gives bonus damage
        m_PlayerStats.m_CurrentDamage += ItemBase.GetSetItemDamage;

        m_PlayerStats.m_CurrentSpeed += ItemBase.GetSetItemSpeed;

        m_PlayerStats.m_CurrentHealth += ItemBase.GetSetItemHealth;
    }

    public void RemoveStats(ItemObjBase removedItem)
    {
        m_PlayerStats.m_CurrentDamage -= removedItem.GetSetItemDamage;

        m_PlayerStats.m_CurrentSpeed -= removedItem.GetSetItemSpeed;

        // Items that give health dont remove them
        //m_PlayerStats.m_CurrentHealth -= removedItem.GetSetItemHealth;
    }

    public void AddToInventory(GameObject itemToAdd)
    {
        // Add item to inventory

        ItemObjBase ItemBase = itemToAdd.GetComponent<ItemObjBase>();

        // Add all items to inventory items
        InventoryItems.Add(ItemBase);

        // If the item is unique
        if (ItemBase.UniqueAbility)
        {
            // Does any unique pick up effects
            // i.e editing stats like dash speed etc
            ItemBase.OnPickUp();

            // Only unique items are added to this list
            UniqueItems.Add(ItemBase);
        }

        AddStats(itemToAdd);

        Debug.Log("item Added" + itemToAdd.name);

        // Will update UI to show the new items
        // When UI is implemented
    }

    public void RemoveFromInventory(ItemObjBase itemToRemove)
    {
        // Remove from inventory item list
        InventoryItems.Remove(itemToRemove);

        if (itemToRemove.UniqueAbility)
        {
            // Remove from unique item list
            UniqueItems.Remove(itemToRemove);

            //Remove any bonuses to stats that were places before
            itemToRemove.OnRemove();
        }
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
        //if (collision.gameObject.tag  == "Item")
        //{
        //    // They can pick it up
        //    Debug.Log("Collided with an item");

        //    // They can pick it up
        //    if (Input.GetKeyDown("E"))
        //    {
        //        EquipmentManager.Instance.PickupItem(collision.gameObject);
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Item")
        //{
        //    // They can pick it up
        //    Debug.Log("Collided with an item");
        //}
    }
}
