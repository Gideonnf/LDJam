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
        TOTAL_ITEMS
    }

    [Tooltip("List of possible Items from normal monsters")]
    [SerializeField] List<Item> m_NormalItemList = new List<Item>();
    [Tooltip("List of possible Items from boss monsters")]
    [SerializeField] List<Item> m_BossItemlist = new List<Item>();
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
    float elapsedTime = 0.0f;

    WeightedObject<Item> m_NormalItems = new WeightedObject<Item>();
    WeightedObject<Item> m_BossItems = new WeightedObject<Item>();
   // WeightedObject<Item> m_TradeItems = new WeightedObject<Item>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < m_NormalItemList.Count; ++i)
        {
            m_NormalItems.AddEntry(m_NormalItemList[i], m_NormalItemList[i].GetSetItemChance);
        }

        for (int i = 0; i < m_BossItemlist.Count; ++i)
        {
            m_BossItems.AddEntry(m_BossItemlist[i], m_BossItemlist[i].GetSetItemChance);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        // Elapsed time increment
        elapsedTime += Time.deltaTime;
    }

    public Item GetNormalItem()
    {
       return  m_NormalItems.GetRandom();
    }

    public Item GetBossItem()
    {
        return m_BossItems.GetRandom();
    }

    /// <summary>
    /// When Enemies die, call for dropping a normal item
    /// If the random chance returns nothing, no item is dropped
    /// </summary>
    /// <param name="DropPosition">The Enemy position when he is killed is where the item will drop</param>
    public void NormalItemDrop(Vector3 DropPosition)
    {
        Item itemDrop = GetNormalItem();

        // No item dropped
        if (itemDrop == null)
        {
            return;
        }
        else
        {
            // Drop the items
        }
    }



}
