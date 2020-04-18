using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public List<ItemObjBase> UniqueItems = new List<ItemObjBase>();
    List<ItemObjBase> InventoryItems = new List<ItemObjBase>();
    PlayerStats m_PlayerStats;

    public GameObject InventoryBarReference;
    public List<GameObject> InventorySlots = new List<GameObject>();

    Dictionary<int, ItemObjBase> ItemSlotTracker = new Dictionary<int, ItemObjBase>();
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();

        foreach (Transform child in InventoryBarReference.transform)
        {
            InventorySlots.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
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

        AddToUI(ItemBase);

        Destroy(itemToAdd);

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
    /// Called when adding an item to the UI Bar
    /// </summary>
    /// <param name="itemToAdd"></param>
    public void AddToUI(ItemObjBase itemToAdd)
    {
        for (int i = 0; i < InventorySlots.Count; ++i)
        {
            // Takes the first inactive item
            if (InventorySlots[i].activeSelf == false)
            {
                // Set the item slot to active
                InventorySlots[i].SetActive(true);
                // Change the sprite
                InventorySlots[i].GetComponent<Image>().sprite = itemToAdd.m_ItemSprite;
                // Store it in the dictionary to keep track 
                ItemSlotTracker.Add(i, itemToAdd);

                break;
            }
        }
    }

    /// <summary>
    /// Removes the item from the UI bar
    /// Called if the player picks up too many items or sells an item
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveFromtUI(ItemObjBase itemToRemove)
    {
        //TODO: Find a way to coordinate item slots with the item its holding(?)
        // So when the player removes it can remove an item in the middle for example
        // then shift all the items down
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
