using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjBase : MonoBehaviour
{
    [Header("Item Data")]

    [Tooltip("Name of the item")]
    [SerializeField] string m_ItemName = "Unnamed Item";

    [Tooltip("Item Description")]
    [TextArea(4, 6)]
    public string m_ItemDescription;

    [Tooltip("Item Sprite")]
    public Sprite m_ItemSprite;

    [Tooltip("Type of item")]
    public EquipmentManager.ItemType m_ItemType;

    [Tooltip("Damage Boost of the item")]
    [SerializeField] float m_DamageBoost;

    [Tooltip("Speed Boost of the item")]
    [SerializeField] float m_SpeedBoost;

    [Tooltip("Health Gain from the item")]
    [SerializeField] int m_HealthGain;

    [Tooltip("Base chance of item drop")]
    [Range(0, 100)]
    [SerializeField] float m_ItemChance;

    [Tooltip("Does this object have a unique ability")]
    public bool UniqueAbility = false;

    public virtual void Attack()
    {
        return;
    }

    public virtual void PassiveEffect()
    {
        return;
    }

    public string GetSetItemName
    {
        get { return m_ItemName; }
        set { m_ItemName = value; }
    }

    public float GetSetItemDamage
    {
        get { return m_DamageBoost; }
        set { m_DamageBoost = value; }
    }

    public float GetSetItemSpeed
    {
        get { return m_SpeedBoost; }
        set { m_SpeedBoost = value; }
    }

    public int GetSetItemHealth
    {
        get { return m_HealthGain; }
        set { m_HealthGain = value; }
    }

    public float GetSetItemChance
    {
        get { return m_ItemChance; }
        set { m_ItemChance = value; }
    }
}
