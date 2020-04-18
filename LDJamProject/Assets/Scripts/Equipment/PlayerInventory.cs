using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<ItemObjBase> InventoryItems = new List<ItemObjBase>();
    List<ItemObjBase> UniqueItems = new List<ItemObjBase>();
    PlayerStats m_PlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Testing");
        if (Input.GetKeyDown(KeyCode.F))
        {
           // for(int i = 0; i < InventoryI)
           for(int i = 0; i < UniqueItems.Count; ++i)
            {
                UniqueItems[i].Attack();
                UniqueItems[i].PassiveEffect();
            }
        }
    }


    public void UpdateStats(GameObject addedItem)
    {

    }

    public void AddToInventory(GameObject itemToAdd)
    {
        // Add item to inventory

        ItemObjBase ItemBase = itemToAdd.GetComponent<ItemObjBase>();

        InventoryItems.Add(ItemBase);

        if (ItemBase.UniqueAbility)
        {
            UniqueItems.Add(ItemBase);
        }

        UpdateStats(itemToAdd);

        Debug.Log("item Added" + itemToAdd.name);

        // Will update UI to show the new items
        // When UI is implemented
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
