﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : SingletonBase<TradeManager>
{
    [Tooltip("Reference to the prefab for trading")]
    public GameObject TradeMenuObject;

    [Tooltip("Prefab for the item slot")]
    public GameObject TradeItemSlot;

    [Tooltip("Prefab for NPC item slot")]
    public GameObject NPCTradeItemSlot;

    [Tooltip("Sprite to show selected objects")]
    public Sprite SelectedObjectSprite;

    [Tooltip("Sprite for unselected objects")]
    public Sprite UnselectedObjectSprite;

    [Tooltip("When the player selects an item to trade, this is the UI Item slot")]
    public GameObject PlayerTradeSlot;

    [Tooltip("When the player selects an item from the NPC, this is the NPC's trade slot")]
    public GameObject NPCTradeSlot;

    [Tooltip("NPC Sprite to change")]
    public GameObject NPCSprite;

    [Tooltip("Player Sprite")]
    public GameObject PlayerSprite;

    [Tooltip("Confirm Button Sprite")]
    public GameObject ConfirmSprite;

    //[Tooltip("Close Button Sprite")]
    //public GameObject CloseSprite;

    [Header("Inventory Configuration for Player")]
    [Tooltip("Starting position of the first item slot")]
    public Vector2 PlayerItemStartingPos;

    [Tooltip("Distance Between each item slots")]
    public Vector2 PlayerSlotDistance = new Vector2(75, 100);

    [Tooltip("How many rows and columns")]
    public Vector2 ColumnRow = new Vector2(6, 2);

    [Header("Inventory Configuration for NPC")]
    [Tooltip("Starting position of the NPC's first item slot")]
    public Vector2 NPCItemStartingPos;

    [Tooltip("Distance between each item slots")]
    public float NPCSlotDistance = 95;


    [HideInInspector] public GameObject currentActivePlayerButton = null;
    [HideInInspector] public GameObject currentActiveNPCButton = null;

    // Keep track of their inventory
    // Inventory slot is created in player Inventory
    List<InventorySlot> PlayerInventorySlots = new List<InventorySlot>();
    List<InventorySlot> NPCInventorySlots = new List<InventorySlot>();

    PlayerInventory m_playerInventoryRef;
    ItemObjBase PlayerItemToTrade;
    ItemObjBase NPCItemToTrade;

    // Start is called before the first frame update
    void Start()
    {
        m_playerInventoryRef = PlayerController.Instance.m_PlayerInventory;
    }

    // Update is called once per frame
    void Update()
    {
        // If both items are active
        if (PlayerTradeSlot.activeSelf == true && NPCTradeSlot.activeSelf == true)
        {
            ConfirmSprite.SetActive(true);
        }
    }

    /// <summary>
    /// When the player interacts with an NPC, Trading is enabled
    /// </summary>
    /// <param name="NPCToTrade">NPC that the player is trading with</param>
    public void EnableTrading(GameObject NPCToTrade)
    {
        // incase it bugs out and keeps opened
        if (TradeMenuObject.activeSelf == true)
            return;


        // Set the trade menu to active
        TradeMenuObject.SetActive(true);

        SetUpTrading(NPCToTrade);
    }

    void TestInventory()
    {
        if (TradeMenuObject.activeSelf == true)
            return;


        // Set the trade menu to active
        TradeMenuObject.SetActive(true);

        CreatePlayerInventory();
    }

    /// <summary>
    /// Sets up the trading menu to the specific NPC
    /// </summary>
    /// <param name="NPCToTrade">The npc that is trading with the player</param>
    void SetUpTrading(GameObject NPCToTrade)
    {
        PlayerSprite.SetActive(true);
        NPCSprite.SetActive(true);

        // Set up NPC Sprite
        NPCSprite.GetComponent<Image>().sprite = NPCToTrade.GetComponent<TradeNPC>().NPCSprite;
        
        // Set up Player Inventory
        CreatePlayerInventory();

        // Set up NPC Inventory
        CreateNPCInventory(NPCToTrade);
    }

    public void CreatePlayerInventory()
    {
        // Just some note
        // Since I might get confused again in the future
        // When adding items into the player's trade inventory
        // since it can only hold 12 at a time
        // I'm using the player's hotbar inventory, InventorySlots, instead of InventoryItems
        // that is because i can do a 1 to 1 comparison and add it over
        // If they player sells an item and it is removed
        // I'll wipe and update it with the same inventory slots

        int currentActiveSlots = 0;

        for (int row = 0; row < ColumnRow.y; ++ row)
        {
            for (int column = 0; column < ColumnRow.x; ++column)
            {
                // Check to make sure that it doesnt go above the item slots
                if (currentActiveSlots >= m_playerInventoryRef.inventorySlots.Count)
                    break;

                ItemObjBase itemAdded = m_playerInventoryRef.inventorySlots[currentActiveSlots].ItemStored;

                // Increment ot keep track of the number of items currently in the inventory

                // Create the slot
                GameObject newUISlot = Instantiate(TradeItemSlot, TradeMenuObject.transform);
                // Adjust the position of the UI slot
                // Vector2 UISlotPosition = newUISlot.GetComponent<RectTransform>().anchoredPosition;
                Vector2 UISlotPosition = PlayerItemStartingPos;
                // Set the new position of the UI Slot
                UISlotPosition.x += (PlayerSlotDistance.x * column);
                UISlotPosition.y -= (PlayerSlotDistance.y * row);

                newUISlot.GetComponent<RectTransform>().anchoredPosition = UISlotPosition;

                newUISlot.GetComponent<TradeItemSlot>().PrepareSlot(itemAdded);

                // take the same amount of quantity from the inventory slots as well
                int Quantity = m_playerInventoryRef.inventorySlots[currentActiveSlots].Quantity;

                // Create an inventory slot
                InventorySlot newInventorySlot = new InventorySlot(newUISlot, itemAdded, Quantity);
                // Add it to the list
                PlayerInventorySlots.Add(newInventorySlot);

                // Increment the current active slots
                currentActiveSlots++;
            }
        }


    }

    public void CreateNPCInventory(GameObject NPCToTrade)
    {
        // Reference to the trade npc script for creating the inventory
        TradeNPC tradeNPC = NPCToTrade.GetComponent<TradeNPC>();

        // Loop through the NPC's item list
        // NPCs will only have 3 items 
        // dont have to check if it goes more than 3 xd
        for(int i = 0; i < tradeNPC.m_NPCItemList.Count; ++i)
        {
            ItemObjBase NPCItem = tradeNPC.m_NPCItemList[i];

            // Create the NPC's item slot
            GameObject newUISlot = Instantiate(NPCTradeItemSlot, TradeMenuObject.transform);
            
            // Change the position
            Vector2 newUISlotPos = newUISlot.GetComponent<RectTransform>().anchoredPosition;
            
            // Set to the starting position
            newUISlotPos = NPCItemStartingPos;

            //Adjust the position
            newUISlotPos.x += NPCSlotDistance * i;

            newUISlot.GetComponent<RectTransform>().anchoredPosition = newUISlotPos;

            newUISlot.GetComponent<TradeItemSlot>().PrepareSlot(NPCItem);

            //Create the inventory slot
            InventorySlot newInventorySlot = new InventorySlot(newUISlot, tradeNPC.m_NPCItemList[i], 1);

            NPCInventorySlots.Add(newInventorySlot);
        }
    }

    public void SelectPlayerItem(GameObject itemSelected)
    {
        for(int i = 0; i < PlayerInventorySlots.Count; ++i)
        {
            if (PlayerInventorySlots[i].SlotUI == itemSelected)
            {
                // Swap the sprite
                PlayerTradeSlot.GetComponent<Image>().sprite = PlayerInventorySlots[i].ItemStored.m_ItemSprite;
                PlayerTradeSlot.SetActive(true);
                PlayerItemToTrade = PlayerInventorySlots[i].ItemStored;
            }
        }
    }

    public void SelectNPCItem(GameObject itemSelected)
    {
        for (int i = 0; i < NPCInventorySlots.Count; ++i)
        {
            if(NPCInventorySlots[i].SlotUI == itemSelected)
            {
                //Swap the sprite
                NPCTradeSlot.GetComponent<Image>().sprite = NPCInventorySlots[i].ItemStored.m_ItemSprite;
                NPCTradeSlot.SetActive(true);
                NPCItemToTrade = NPCInventorySlots[i].ItemStored;
            }
        }
    }

    /// <summary>
    /// For when the player confirms a trade
    /// </summary>
    /// <param name="ItemComingIn"> The NPC's item that the player is getting</param>
    /// <param name="ItemGoingOut"> The Player's Item that the player is losing to the NPC</param>
    void TradeItem(ItemObjBase ItemComingIn, ItemObjBase ItemGoingOut)
    {
        PlayerTradeSlot.SetActive(false);
        NPCTradeSlot.SetActive(false);

        // Add the item coming in to the player inventory
        m_playerInventoryRef.AddToInventory(ItemComingIn.gameObject, false);
        // Remove the item that is going out
        m_playerInventoryRef.RemoveFromInventory(ItemGoingOut);
        // Remove item from NPC

        // Refresh the TradeUI inventories
        ClearPlayerInventory();

        CreatePlayerInventory();

        // set the sprite back lol
        currentActiveNPCButton.GetComponent<Image>().sprite = UnselectedObjectSprite;

        // reset the active buttons
        currentActiveNPCButton = null;
        currentActivePlayerButton = null;

        //ClearNPCInventory();

        //CreateNPCInventory();
    }

    /// <summary>
    /// Just for the button press onclick event
    /// </summary>
    public void ConfirmButtonClick()
    {
        TradeItem(NPCItemToTrade, PlayerItemToTrade);
    }

    public void CloseTrade()
    {
        ClearNPCInventory();
        ClearPlayerInventory();

        PlayerTradeSlot.SetActive(false);
        NPCTradeSlot.SetActive(false);

        PlayerSprite.SetActive(false);
        NPCSprite.SetActive(false);

        TradeMenuObject.SetActive(false);
    }

    void ClearPlayerInventory()
    {
        for(int i = 0; i < PlayerInventorySlots.Count; ++i)
        {
            Destroy(PlayerInventorySlots[i].SlotUI);
        }

        PlayerInventorySlots.Clear();
    }

    void ClearNPCInventory()
    {
        for (int i = 0; i < NPCInventorySlots.Count; ++i)
        {
            Destroy(NPCInventorySlots[i].SlotUI);
        }

        NPCInventorySlots.Clear();
    }
}
