using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : ItemObjBase
{
    PlayerStats playerStats;

    public override void OnPickUp()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        playerStats = tempPlayer.GetComponent<PlayerStats>();
        base.OnPickUp();
    }

    public override void WhenEnemyHit(GameObject enemy)
    {
        if(Random.Range(0,7) == 0)
        {
            if (playerStats.m_CurrentHealth < playerStats.m_MaxHealth)
                playerStats.m_CurrentHealth++;
        }
        base.WhenEnemyHit(enemy);
    }
}
