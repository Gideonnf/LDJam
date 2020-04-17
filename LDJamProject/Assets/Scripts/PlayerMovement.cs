 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody PlayerRB;

    Vector3 movement;

    [SerializeField] float MovementSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
       
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + movement * MovementSpeed * Time.fixedDeltaTime);
    }

    #region Movement Functions

    public void Jump()
    {
        
    }

    public void MoveUp()
    {

    }

    public void MoveLeft()
    {

    }

    public void MoveRight()
    {

    }

    public void MoveDown()
    {

    }

    #endregion

}
