using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    Animator chestAnim;
    public Sprite OpenChestSprite;
    // Start is called before the first frame update
    void Start()
    {
        chestAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(chestAnim.GetCurrentAnimatorStateInfo(0).IsName("ChestOpening"))
        //{
        //    GetComponent<SpriteRenderer>().sprite = OpenChestSprite;
        //}
    }

    public void ChestOpened()
    {
        EquipmentManager.Instance.AlwaysGetItemDrop(gameObject.transform.position);
        SoundManager.Instance.Play("ChestOpen");
        Destroy(gameObject);
    }
}
