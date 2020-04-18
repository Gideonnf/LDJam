 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float dashSpeed = 8.0f;
    [SerializeField] float dashDistance = 5.0f;
    [SerializeField] float timeToRechargeOneDash = 2.0f;
    [SerializeField] int maxNumOfDash = 4;
    [SerializeField] Slider staminaBar;

    Rigidbody2D PlayerRB;
    Vector2 movement;
    bool isDashing;
    float currSpeed;
    float distanceDashed;
    int numOfDash;
    float dashRechargeTime;

    public Vector3 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        isDashing = false;
        currSpeed = movementSpeed;
        distanceDashed = 0;
        numOfDash = maxNumOfDash;
        dashRechargeTime = 0;
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

        if(numOfDash < maxNumOfDash && dashRechargeTime < timeToRechargeOneDash)
        {
            dashRechargeTime += Time.deltaTime;
            if (dashRechargeTime >= timeToRechargeOneDash)
            {
                dashRechargeTime = 0;
                numOfDash++;
            }
        }

        staminaBar.value = (((float)numOfDash * timeToRechargeOneDash) + dashRechargeTime) / (timeToRechargeOneDash * (float)maxNumOfDash);
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + movement * currSpeed * Time.fixedDeltaTime);
    }

    public void Dash()
    {
        if (numOfDash > 0) 
        {
            isDashing = true;
            currSpeed = dashSpeed;
            distanceDashed = 0;
            numOfDash--;
        }
    }
}
