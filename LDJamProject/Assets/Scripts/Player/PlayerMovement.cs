 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Slider staminaBar;

    PlayerStats playerStats;
    Rigidbody2D PlayerRB;
    Vector2 movement;
    bool isDashing;
    float distanceDashed;
    int numOfDash;
    float dashRechargeTime;

    public Vector3 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        PlayerRB = GetComponent<Rigidbody2D>();
        isDashing = false;
        distanceDashed = 0;
        numOfDash = playerStats.m_CurrentMaxDash;
        dashRechargeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing)
        {
            distanceDashed += playerStats.m_CurrentDashSpeed * Time.deltaTime;
            if(distanceDashed >= playerStats.m_CurrentDashDistance)
            {
                isDashing = false;
                playerStats.m_CurrentSpeed = playerStats.m_CurrentMovementSpeed;
                for (int i = 0; i < GetComponent<PlayerInventory>().UniqueItems.Count; ++i)
                {
                    GetComponent<PlayerInventory>().UniqueItems[i].WhenDashEnds();
                }
            }
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movementDir = movement;
        }

        if(numOfDash < playerStats.m_CurrentMaxDash && dashRechargeTime < playerStats.m_CurrentTimeToRechargeOneDash)
        {
            dashRechargeTime += Time.deltaTime;
            if (dashRechargeTime >= playerStats.m_CurrentTimeToRechargeOneDash)
            {
                dashRechargeTime = 0;
                numOfDash++;
            }
        }

        staminaBar.value = (((float)numOfDash * playerStats.m_CurrentTimeToRechargeOneDash) + dashRechargeTime) / (playerStats.m_CurrentTimeToRechargeOneDash * (float)playerStats.m_CurrentMaxDash);
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + movement * playerStats.m_CurrentSpeed * Time.fixedDeltaTime);
    }

    public void Dash()
    {
        if (numOfDash > 0) 
        {
            isDashing = true;
            playerStats.m_CurrentSpeed = playerStats.m_CurrentDashSpeed;
            distanceDashed = 0;
            numOfDash--;
        }
    }
}
