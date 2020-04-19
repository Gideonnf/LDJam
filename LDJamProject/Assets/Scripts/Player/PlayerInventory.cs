using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot
{
    public GameObject SlotUI;
    public ItemObjBase ItemStored;
    public int Quantity;

    public InventorySlot()
    {

    }

    public InventorySlot(GameObject _SlotUI, ItemObjBase _ItemStored, int _Quantity)
    {
        SlotUI = _SlotUI;
        ItemStored = _ItemStored;
        Quantity = _Quantity;
    }
}

public class PlayerInventory : MonoBehaviour
{

    [HideInInspector] public List<ItemObjBase> UniqueItems = new List<ItemObjBase>();
    [HideInInspector] public List<ItemObjBase> InventoryItems = new List<ItemObjBase>();
    PlayerStats m_PlayerStats;

    [Header("Hot bar Settings")]
    [Tooltip("Reference to the object that the UI slots are parented under")]
    public GameObject InventoryBarReference;
    [Tooltip("Reference to the prefab for the UI Slots")]
    public GameObject UISlotPrefab;
    [Tooltip("The distance between each slot")]
    public float SlotDistance = 90;

    [HideInInspector] public float m_PlayerMoney;

    [HideInInspector] public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    int currentInventoryCount;

    //public List<GameObject> InventorySlots = new List<GameObject>();


    //Dictionary<int, ItemObjBase> ItemSlotTracker = new Dictionary<int, ItemObjBase>();
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();

        //foreach (Transform child in InventoryBarReference.transform)
        //{
        //    InventorySlots.Add(child.gameObject);
        //    child.gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < UniqueItems.Count; ++i)
        //{
        //    UniqueItems[i].PassiveEffect();
        //}

        if (Input.GetKeyDown(KeyCode.Z))
        {
            RemoveFromUI(inventorySlots[1].ItemStored);
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

     // I READ FANFICTION FOR ABIT

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

        // Add the item stat bonuses to the player
        AddStats(itemToAdd);
        // Add the item into the UI Hotbar
        AddToUI(itemToAdd);

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
    public void AddToUI(GameObject itemToAdd)
    {
        ItemObjBase itemAdded = itemToAdd.GetComponent<ItemObjBase>();

        for(int i = 0; i < inventorySlots.Count; ++i)
        {
            // If the item exist in the current inventory slot
            if (inventorySlots[i].ItemStored.GetSetItemName == itemAdded.GetSetItemName)
            {
                // Add it to the current inventory slot
                // Debug.Log("item already in hotbar");
                inventorySlots[i].Quantity++;
                // change the quantity number on the object
                inventorySlots[i].SlotUI.GetComponentInChildren<Text>().text = inventorySlots[i].Quantity.ToString();

                return;
            }
        }

        // else then it isnt inside and it has to be added
        GameObject newUISlot = Instantiate(UISlotPrefab, InventoryBarReference.transform);
        //Adjust the position of the UI Slot
        Vector2 UISlotPosition = newUISlot.GetComponent<RectTransform>().anchoredPosition;
        // Set the new position of hte UI Slot
        UISlotPosition.x += (SlotDistance * currentInventoryCount);
        // Change to the new position
        newUISlot.GetComponent<RectTransform>().anchoredPosition = UISlotPosition;


        // Create a new inventory slot
        InventorySlot inventorySlot = new InventorySlot(newUISlot, itemAdded, 1);
        //Add it to the list
        inventorySlots.Add(inventorySlot);
        // Increment the count
        currentInventoryCount++;
    }

    /// <summary>
    /// Removes the item from the UI bar
    /// Called if the player picks up too many items or sells an item
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveFromUI(ItemObjBase itemToRemove)
    {
        for (int i = 0; i < inventorySlots.Count; ++i)
        {
            // If the item exist in the inventory
            if (inventorySlots[i].ItemStored.GetSetItemName == itemToRemove.GetSetItemName)
            {
                // if there are more than 1 quantity of the item
                if (inventorySlots[i].Quantity > 1)
                {
                    // Just reduce the quantity
                    inventorySlots[i].Quantity--;
                    inventorySlots[i].SlotUI.GetComponentInChildren<Text>().text = inventorySlots[i].Quantity.ToString();
                }
                else
                {
                    // Tricky part
                    // Delete the item from the hot bar and shift the remaining
                    // Destroy the UI element first
                    Destroy(inventorySlots[i].SlotUI);

                    // Shift everything down
                    // Start from the element after the item slot that is being deleted
                    for (int x = i + 1; x < inventorySlots.Count; ++x)
                    {
                        // If the current slot is empty already
                        // in the case of deleting the last item, the next item is null already
                        if (inventorySlots[x] == null)
                            continue;

                        Vector2 newUISlot = inventorySlots[x].SlotUI.GetComponent<RectTransform>().anchoredPosition;
                        newUISlot.x -= SlotDistance; // Shift to the left
                        inventorySlots[x].SlotUI.GetComponent<RectTransform>().anchoredPosition = newUISlot;
                    }

                    // Remove from list
                    inventorySlots.Remove(inventorySlots[i]);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// for alvin :cry:
    /// </summary>
    /// <returns></returns>
    public Sprite GetStrongestWeapon()
    {
        ItemObjBase currStrongestWeapon = null;
        for (int i = 0; i < InventoryItems.Count; ++i)
        {
            // If the item is a sword
            if (InventoryItems[i].m_ItemType == EquipmentManager.ItemType.SWORD)
            {
                if (currStrongestWeapon == null)
                {
                    // set the strongest weapon sprite to that weapon
                    currStrongestWeapon = InventoryItems[i];
                }
                else
                {
                    // de item is stronger than de current item
                    // hurr durr
                    if (InventoryItems[i].GetSetItemDamage > currStrongestWeapon.GetSetItemDamage)
                    {
                        currStrongestWeapon = InventoryItems[i];
                    }
                }
            }
        }

        // return the sprite lol
        return currStrongestWeapon.m_ItemSprite;
    }

    /// <summary>
    /// For Angie :cry:
    /// Gives the current money added with the item money value
    /// </summary>
    /// <returns></returns>
    public float GetTotalMoney()
    {
        float totalMoney = 0;

        // Base amount is the player's currnet money count
        totalMoney = m_PlayerMoney;

        return totalMoney;
    }
}
