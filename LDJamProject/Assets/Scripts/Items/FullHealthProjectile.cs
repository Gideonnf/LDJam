using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealthProjectile : MonoBehaviour
{
    bool healthIsMax;
    PlayerStats playerStats;
    float prevAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
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
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.m_CurrentHealth == playerStats.m_MaxHealth && !healthIsMax)
        {
            healthIsMax = true;
            playerStats.m_CurrentRangedAttackSpeed -= 8f;
            if (playerStats.m_CurrentRangedAttackSpeed < playerStats.m_FastestRangedAttackSpeed)
                playerStats.m_CurrentRangedAttackSpeed = playerStats.m_FastestRangedAttackSpeed;
        }
        else if(playerStats.m_CurrentHealth != playerStats.m_MaxHealth && healthIsMax)
        {
            healthIsMax = false;
            playerStats.m_CurrentRangedAttackSpeed = prevAttackSpeed;
        }
    }
}
