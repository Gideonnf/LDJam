using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Tooltip("How long the item last on the ground")]
    [SerializeField] float m_LifeTime = 480;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_LifeTime -= Time.deltaTime;
        if (m_LifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // They can pick it up
            // Debug.Log("Collided with an item");
            
            // They can pick it up
            if (Input.GetKeyDown(KeyCode.E))
            {
                EquipmentManager.Instance.PickupItem(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // They can pick it up
          //  Debug.Log("Collided with an item");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // They can pick it up
           // Debug.Log("Left Collided with an item");
        }
    }

}
