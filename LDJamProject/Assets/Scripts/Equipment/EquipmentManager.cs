using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : SingletonBase<EquipmentManager>
{
    public enum ItemType
    {
        NOTHING,
        RING,
        SWORD,
        BOOTS,
        AMULET,
        ARMOR,
        SHIELD,
        TOTAL_ITEMS
    }

    [Tooltip("List of possible Items from normal monsters")]
    [SerializeField] List<ItemObjBase> m_NormalItemList = new List<ItemObjBase>();
    [Tooltip("List of possible Items from boss monsters")]
    [SerializeField] List<ItemObjBase> m_BossItemlist = new List<ItemObjBase>();
    //[Tooltip("List of possible Items")]
    //[SerializeField] List<Item> m_NormalItemList = new List<Item>();
    [Header("Equipment Manager Configuration")]
    

    [Tooltip("Enable to remove item from the list after dropping")]
    [SerializeField] bool m_DropOnce = false;

    [Tooltip("Money drop from enemies")]
    [SerializeField] float m_MoneyDrop; 

    // Any other configuration I can think of will go here later


    // Keep tracks of how long since an item was dropped
    // The longer the time, the higher the chance of an item drop
   // float elapsedTime = 0.0f;

    WeightedObject<GameObject> m_NormalItems = new WeightedObject<GameObject>();
    WeightedObject<GameObject> m_BossItems = new WeightedObject<GameObject>();
   // WeightedObject<Item> m_TradeItems = new WeightedObject<Item>();

     //Dictionary<GameObject, Item> ActiveItems = new Dictionary<GameObject, Item>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject EmptyItem = new GameObject();
        //ItemObjBase itemObjBase = EmptyItem.GetComponent<ItemObjBase>();
        //itemObjBase.GetSetItemName = "No Items";
        //itemObjBase.m_ItemType = ItemType.NOTHING;

        

      //  float EmptyItemChance = 0;
        for (int i = 0; i < m_NormalItemList.Count; ++i)
        {
            m_NormalItems.AddEntry(m_NormalItemList[i].gameObject, m_NormalItemList[i].GetSetItemChance);
          //  EmptyItemChance += m_NormalItemList[i].GetSetItemChance;
        }

        // Set the chance of receiving no items as half of the total accumulated chance to have an item
        //itemObjBase.GetSetItemChance = (EmptyItemChance / 2);
        // Add the empty item into the item list
       // m_NormalItems.AddEntry(EmptyItem, itemObjBase.GetSetItemChance);

        // No empty items for bosses as boss will always drop an item
        for (int i = 0; i < m_BossItemlist.Count; ++i)
        {
            m_BossItems.AddEntry(m_BossItemlist[i].gameObject, m_BossItemlist[i].GetSetItemChance);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Elapsed time increment
        //elapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// When Enemies die, call for dropping a normal item
    /// If the random chance returns nothing, no item is dropped
    /// </summary>
    /// <param name="DropPosition">The Enemy position when he is killed is where the item will drop</param>
    public void NormalItemDrop(Vector3 DropPosition)
    {
        GameObject item = m_NormalItems.GetRandom();

        if (item == null)
        {
            Debug.Log("No item was found");
            return;
        }

        ItemObjBase objBase = item.GetComponent<ItemObjBase>();

        if (objBase.m_ItemType == ItemType.NOTHING)
        {
            // Tehres no item
            return;
        }
        else
        {
            // Drop the item
            // Create the game object
            // Cant use object pooler cause theres too many different items
            GameObject newItem = Instantiate(item);

            // Set the position
            newItem.transform.position = DropPosition;

            Debug.Log("Spawned Item : " + objBase.name);
        }
    }


    public void PickupItem(GameObject Item)
    {
       // Item pickedupItem;
       // ActiveItems.TryGetValue(Item, out pickedupItem);

        // If they manage to pick up the item
        if (Item != null)
        {
            // Add it to player inventory
            // Call player class here later
            PlayerController.Instance.m_PlayerInventory.AddToInventory(Item);

            // Remove it from the list and set active to false
            //ActiveItems.Remove(Item);

           // Destroy(Item);
        }

    }

}
