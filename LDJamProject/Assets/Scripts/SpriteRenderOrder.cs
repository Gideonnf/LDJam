using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
