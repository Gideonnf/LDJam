﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Slider staminaBar;
    [SerializeField] float timeInactiveToBeIdle = 0.1f;
    [SerializeField] GameObject ghostDash;
    [SerializeField] float timeBeforeGhostSpawn;
    [SerializeField] float timeBetweenFootstepSFX;

    PlayerStats playerStats;
    Rigidbody2D PlayerRB;
    Vector2 movement;
    public bool isDashing;
    float distanceDashed;
    int numOfDash;
    float dashRechargeTime;
    Animator playerAnimator;

    public bool isPullingCaravan;
    public bool isAttackDashing;
    public bool isPickingUpCaravan;

    public enum FaceDirection
    {
        up,
        down,
        left,
        right
    }
    public FaceDirection playerFaceDir;
    float distanceAttackDashed;
    float attackDashTimer;
    float idleTimer;
    float footstepSFXTimer;

    float ghostSpawnTimer;

    public Vector3 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        PlayerRB = GetComponent<Rigidbody2D>();
        isDashing = false;
        isAttackDashing = false;
        distanceDashed = 0;
        distanceAttackDashed = 0;
        attackDashTimer = 0;
        numOfDash = playerStats.m_CurrentMaxDash;
        dashRechargeTime = 0;
        idleTimer = 0;
        playerAnimator = GetComponent<Animator>();
        ghostSpawnTimer = 0;
        isPullingCaravan = false;
        footstepSFXTimer = 0;
        isPickingUpCaravan = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickingUpCaravan)
        {
            movement = Vector2.zero;
            playerAnimator.SetBool("IsIdling", true);
            playerAnimator.SetFloat("XSpeed", movement.x);
            playerAnimator.SetFloat("YSpeed", movement.y);
        }
        else if(isAttackDashing)
        {
            if (distanceAttackDashed >= playerStats.m_CurrentAttackDashDistance)
            {
                playerStats.m_CurrentSpeed = playerStats.m_CurrentMovementSpeed;
                movement = Vector3.zero;
            }
            else
            {
                distanceAttackDashed += playerStats.m_CurrentAttackDashSpeed * Time.deltaTime;
            }

            attackDashTimer += Time.deltaTime;
            if(attackDashTimer >= playerStats.m_CurrentAttackDashTime)
            {
                isAttackDashing = false;
            }
        }
        else if (isDashing)
        {
            distanceDashed += playerStats.m_CurrentDashSpeed * Time.deltaTime;
            ghostSpawnTimer += Time.deltaTime;
            if(ghostSpawnTimer >= timeBeforeGhostSpawn)
            {
                ghostSpawnTimer = 0;
                GameObject temp = Instantiate(ghostDash, transform.position, Quaternion.identity);
                temp.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                Destroy(temp, 0.2f);
            }
            if(distanceDashed >= playerStats.m_CurrentDashDistance)
            {
                isDashing = false;
                playerStats.m_CurrentSpeed = playerStats.m_CurrentMovementSpeed;
                for (int i = 0; i < GetComponent<PlayerInventory>().UniqueItems.Count; ++i)
                {
                    GetComponent<PlayerInventory>().UniqueItems[i].WhenDashEnds();
                }
                playerAnimator.SetBool("IsDashing", false);
            }
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movementDir = movement;
            playerAnimator.SetFloat("XSpeed", movement.x);
            playerAnimator.SetFloat("YSpeed", movement.y);
            if(movement == Vector2.zero)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= timeInactiveToBeIdle)
                    playerAnimator.SetBool("IsIdling", true);
            }
            else
            {
                footstepSFXTimer += Time.deltaTime;
                if(footstepSFXTimer >= timeBetweenFootstepSFX)
                {
                    footstepSFXTimer = 0;
                    switch(Random.Range(0,4))
                    {
                        case 0:
                            SoundManager.Instance.Play("PlayerRun1");
                            break;
                        case 1:
                            SoundManager.Instance.Play("PlayerRun2");
                            break;
                        case 2:
                            SoundManager.Instance.Play("PlayerRun3");
                            break;
                        case 3:
                            SoundManager.Instance.Play("PlayerRun4");
                            break;
                    }
                }
                idleTimer = 0;
                playerAnimator.SetBool("IsIdling", false);
            }
            movement.Normalize();
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
        if (numOfDash > 0 && !isAttackDashing) 
        {
            SoundManager.Instance.Play("PlayerDash");
            isDashing = true;
            playerStats.m_CurrentSpeed = playerStats.m_CurrentDashSpeed;
            distanceDashed = 0;
            numOfDash--;
            playerAnimator.SetBool("IsDashing", true);
        }
    }

    public void MeleeAttackDash()
    {
        isAttackDashing = true;
        playerStats.m_CurrentSpeed = playerStats.m_CurrentAttackDashSpeed;
        distanceAttackDashed = 0;
        attackDashTimer = 0;
        movement = GetComponent<PlayerCombat>().playerLookDir;
        movement.Normalize();
    }
}
