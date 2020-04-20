using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardTradeSlot : MonoBehaviour
{
    public GameObject itemSprite;

    WizardMenu wizardMenu;

    ItemObjBase storedItem;

    [HideInInspector] public bool itemSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        wizardMenu = transform.parent.parent.gameObject.GetComponent<WizardMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareSlot(ItemObjBase AddedItem)
    {
        if (AddedItem.m_ItemType == EquipmentManager.ItemType.SWORD)
        {
            itemSprite.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -45));
        }

        storedItem = AddedItem;
        itemSprite.GetComponent<Image>().sprite = AddedItem.m_ItemSprite;
    }

    public void SelectItem()
    {
        // If false, set to true, if true set to false
        itemSelected = !itemSelected;

        // if it is true
        if (itemSelected)
        {
            //Add it to the list
            wizardMenu.SelectedItems.Add(storedItem);

            this.gameObject.GetComponent<Image>().sprite = wizardMenu.SelectedObjectSprite;
        }
        else
        {
            // Remove from the list
            wizardMenu.SelectedItems.Remove(storedItem);

            this.gameObject.GetComponent<Image>().sprite = wizardMenu.UnselectedObjectSprite;
        }
    }
}
