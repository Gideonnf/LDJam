 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float dashSpeed = 8.0f;
    [SerializeField] float dashDistance = 5.0f;

    Rigidbody2D PlayerRB;
    Vector2 movement;
    bool isDashing;
    float currSpeed;
    float distanceDashed;

    public Vector3 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        isDashing = false;
        currSpeed = movementSpeed;
        distanceDashed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing)
        {
            distanceDashed += dashSpeed * Time.deltaTime;
            if(distanceDashed >= dashDistance)
            {
                isDashing = false;
                currSpeed = movementSpeed;
            }
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movementDir = movement;
        }

    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + movement * currSpeed * Time.fixedDeltaTime);
    }

    public void Dash()
    {
        isDashing = true;
        currSpeed = dashSpeed;
        distanceDashed = 0;
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
