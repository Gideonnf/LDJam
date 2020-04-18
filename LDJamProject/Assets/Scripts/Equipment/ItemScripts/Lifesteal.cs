using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : ItemObjBase
{
    PlayerStats playerStats;

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
        base.PassiveEffect();
    }

    public override void OnPickUp()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        playerStats = tempPlayer.GetComponent<PlayerStats>();
        base.OnPickUp();
    }

    public override void OnRemove()
    {
        base.OnRemove();
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
