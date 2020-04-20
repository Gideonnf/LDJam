using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItemSlot : MonoBehaviour
{
    public GameObject itemSprite;
    public bool IsPlayerSlot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareSlot(ItemObjBase ItemSprite)
    {
        // if its a sword, it should be rotated
        if (ItemSprite.m_ItemType == EquipmentManager.ItemType.SWORD)
        {
            itemSprite.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -45));
        }

        itemSprite.GetComponent<Image>().sprite = ItemSprite.m_ItemSprite;
    }

    public void SelectItem()
    {
        if (IsPlayerSlot)
        {
            SwapPlayerButtonSprite();
            TradeManager.Instance.SelectPlayerItem(this.gameObject);
        }
        else
        {
            SwapNPCButtonSprite();
            TradeManager.Instance.SelectNPCItem(this.gameObject);
        }
    }

    void SwapPlayerButtonSprite()
    {
        if (TradeManager.Instance.currentActivePlayerButton == null)
        {
            TradeManager.Instance.currentActivePlayerButton = this.gameObject;
            this.gameObject.GetComponent<Image>().sprite = TradeManager.Instance.SelectedObjectSprite;
        }
        // If the palyer selects another item from the player inventory
        else if (TradeManager.Instance.currentActivePlayerButton != this.gameObject)
        {
            // Set to the unselected sprite 
            TradeManager.Instance.currentActivePlayerButton.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
            TradeManager.Instance.currentActivePlayerButton = this.gameObject;
            this.gameObject.GetComponent<Image>().sprite = TradeManager.Instance.SelectedObjectSprite;

        }
    }

    void SwapNPCButtonSprite()
    {
        if (TradeManager.Instance.currentActiveNPCButton == null)
        {
            TradeManager.Instance.currentActiveNPCButton = this.gameObject;
            this.gameObject.GetComponent<Image>().sprite = TradeManager.Instance.SelectedObjectSprite;
        }
        // If the palyer selects another item from the player inventory
        else if (TradeManager.Instance.currentActiveNPCButton != this.gameObject)
        {
            // Set to the unselected sprite 
            TradeManager.Instance.currentActiveNPCButton.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
            TradeManager.Instance.currentActiveNPCButton = this.gameObject;
            this.gameObject.GetComponent<Image>().sprite = TradeManager.Instance.SelectedObjectSprite;

        }

    }


}
