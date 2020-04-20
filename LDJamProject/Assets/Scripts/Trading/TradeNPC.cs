using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeNPC : MonoBehaviour
{
    [Tooltip("Inventory of the NPC")]
    public List<ItemObjBase> m_NPCItemList = new List<ItemObjBase>();

    [Tooltip("Sprite of the NPC")]
    public Sprite NPCSprite;

    // For the randomisation
    WeightedObject<ItemObjBase> m_NPCItems = new WeightedObject<ItemObjBase>();

    bool PlayerInRange = false;

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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);

        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TradeManager.Instance.EnableTrading(this.gameObject);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    // If the player is in collision with the NPC's interaction range
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Colliding with player");
    //        PlayerInRange = true;
    //        // If a key is pressed
    //        //if(Input.GetKeyDown(KeyCode.F))
    //        //{
    //        //    TradeManager.Instance.EnableTrading(this.gameObject);
    //        //}
    //        // Enable trade
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colliding with player");

            PlayerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("OINAIOGNHIOAGOASGIOADSGHB");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInRange = false;

        }
    }
}
