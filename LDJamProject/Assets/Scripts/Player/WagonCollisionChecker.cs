using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonCollisionChecker : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<Wagon>().isColliding = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
            transform.parent.GetComponent<Wagon>().isColliding = true;
    }
}
