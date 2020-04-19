﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeNPC : MonoBehaviour
{
    [Tooltip("Inventory of the NPC")]
    public List<ItemObjBase> m_NPCItemList = new List<ItemObjBase>();

    // For the randomisation
    WeightedObject<ItemObjBase> m_NPCItems = new WeightedObject<ItemObjBase>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < m_NPCItemList.Count; ++i)
        {
            m_NPCItems.AddEntry(m_NPCItemList[i], m_NPCItemList[i].GetSetItemChance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the player is in collision with the NPC's interaction range
        if (collision.gameObject.tag == "Player")
        {
            // If a key is pressed
            // Enable trade
        }
    }
}
