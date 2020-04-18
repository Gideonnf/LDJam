using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealthProjectile : ItemObjBase
{
    bool healthIsMax;
    PlayerStats playerStats;
    float prevAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void PassiveEffect()
    {
        if (playerStats.m_CurrentHealth == playerStats.m_MaxHealth && !healthIsMax)
        {
            healthIsMax = true;
            playerStats.m_CurrentRangedAttackSpeed -= 8f;
            if (playerStats.m_CurrentRangedAttackSpeed < playerStats.m_FastestRangedAttackSpeed)
                playerStats.m_CurrentRangedAttackSpeed = playerStats.m_FastestRangedAttackSpeed;
        }
        else if (playerStats.m_CurrentHealth != playerStats.m_MaxHealth && healthIsMax)
        {
            healthIsMax = false;
            playerStats.m_CurrentRangedAttackSpeed = prevAttackSpeed;
        }
        base.PassiveEffect();
    }

    public override void OnPickUp()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        playerStats = tempPlayer.GetComponent<PlayerStats>();
        prevAttackSpeed = playerStats.m_CurrentRangedAttackSpeed;
        if (playerStats.m_CurrentHealth == playerStats.m_MaxHealth)
        {
            healthIsMax = true;
            playerStats.m_CurrentRangedAttackSpeed -= 8f;
            if (playerStats.m_CurrentRangedAttackSpeed < playerStats.m_FastestRangedAttackSpeed)
                playerStats.m_CurrentRangedAttackSpeed = playerStats.m_FastestRangedAttackSpeed;
        }
        else
            healthIsMax = false;

        base.OnPickUp();
    }

    public override void OnRemove()
    {
        if(healthIsMax)
            playerStats.m_CurrentRangedAttackSpeed = prevAttackSpeed;
        base.OnRemove();
    }
}
