using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WizardMenu : MonoBehaviour
{
    [Tooltip("Reference to the game object for wizard trading")]
    public GameObject WizardTradeObject;

    [Tooltip("Reference to the Money text object")]
    public GameObject MoneyDisplay;

    [Tooltip("Reference to the prefab for inventory slot")]
    public GameObject InventoryItemSlot;

    [Tooltip("Sprite to show selected objects")]
    public Sprite SelectedObjectSprite;

    [Tooltip("Sprite to show unselected objects")]
    public Sprite UnselectedObjectSprite;

    [Header("Inventory Configuration")]
    [Tooltip("Starting position for hte first item slot")]
    public Vector2 ItemStartingPos;

    [Tooltip("Distance between each item slots")]
    public Vector2 PlayerSlotDistance = new Vector2(75, 100);

    [Tooltip("How many rows and columns")]
    public Vector2 ColumnRow = new Vector2(3, 4);

    [HideInInspector] public List<ItemObjBase> SelectedItems = new List<ItemObjBase>();

    List<InventorySlot> InventorySlots = new List<InventorySlot>();

    PlayerInventory m_Inventory;

    float MoneyFromitems = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Inventory = PlayerController.Instance.m_PlayerInventory;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenUI();
        }

        float tempMoney = 0;
        // Loop through the selected items to get the total money
        for(int i = 0; i < SelectedItems.Count; ++i)
        {
            tempMoney += SelectedItems[i].m_ItemPrice;
        }

        MoneyFromitems = (tempMoney + m_Inventory.m_PlayerMoney);

        MoneyDisplay.GetComponent<TextMeshProUGUI>().text = MoneyFromitems.ToString();
    }

    /// <summary>
    /// When the conversation is done
    /// This function will open up the UI Screen
    /// </summary>
    public void OpenUI()
    {
        WizardTradeObject.SetActive(true);

        CreateInventory();
    }

    public void CloseMenu()
    {
        ClearInventory();
        WizardTradeObject.SetActive(false);
    }

    void ClearInventory()
    {
        for(int i = 0; i < InventorySlots.Count; ++i)
        {
            Destroy(InventorySlots[i].SlotUI);
        }

        InventorySlots.Clear();
    }

    void CreateInventory()
    {
        int currentActiveSlots = 0;

        for (int row = 0; row < ColumnRow.y; ++row)
        {
            for (int column = 0; column < ColumnRow.x; ++column)
            {
                if (currentActiveSlots >= m_Inventory.inventorySlots.Count)
                    break;

                // Store the item to be added
                ItemObjBase itemAdded = m_Inventory.inventorySlots[currentActiveSlots].ItemStored;

                // Create the UI Slot
                GameObject newUISlot = Instantiate(InventoryItemSlot, WizardTradeObject.transform);

                // Adjust the position of the item
                Vector2 UISlotPosition = ItemStartingPos;

                UISlotPosition.x += (PlayerSlotDistance.x * column);
                UISlotPosition.y -= (PlayerSlotDistance.y * row);

                newUISlot.GetComponent<RectTransform>().anchoredPosition = UISlotPosition;

                // Prepare the item slot (i.e add the sprite to it)
                //newUISlot.GetComponent<>();
                newUISlot.GetComponent<WizardTradeSlot>().PrepareSlot(itemAdded);

                int Quantity = m_Inventory.inventorySlots[currentActiveSlots].Quantity;
                // Create a inventory slot
                InventorySlot newInventorySlot = new InventorySlot(newUISlot, itemAdded, Quantity);
                // Add to the list
                InventorySlots.Add(newInventorySlot);

                currentActiveSlots++;
            }
        }
    }
}
