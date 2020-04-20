using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItemSlot : MonoBehaviour
{
    public GameObject itemSprite;
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
}
