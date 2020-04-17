using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerMovement m_PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        // Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }


    /// <summary>
    /// For any interaction with E Key
    /// </summary>
    public void Interact()
    {

    }

    /// <summary>
    /// Flipping the sprite since its 2D
    /// </summary>
    public void Flip()
    {

    }

}
