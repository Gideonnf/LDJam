using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lifesteal : ItemObjBase
{
    PlayerStats playerStats;
    GameObject lifestealPet_;
    [SerializeField] GameObject lifestealPet;

    public override void OnPickUp()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        playerStats = tempPlayer.GetComponent<PlayerStats>();
        GameObject spawnPos = GameObject.FindGameObjectWithTag("PetSpawnPos");
        lifestealPet_ = Instantiate(lifestealPet, spawnPos.transform.position, Quaternion.identity);
        lifestealPet_.GetComponent<NavMeshAgent>().stoppingDistance = playerStats.m_NumOfPets * playerStats.m_DistanceBetweenEachPet;
        lifestealPet_.GetComponent<FollowBehindPlayer>().destination = spawnPos;
        playerStats.m_NumOfPets++;
        base.OnPickUp();
    }

    public override void WhenEnemyHit(GameObject enemy)
    {
        if(Random.Range(0,7) == 0)
        {
            if (playerStats.m_CurrentHealth < playerStats.m_MaxHealth)
            {
                SoundManager.Instance.Play("BatTrigger");
                playerStats.m_CurrentHealth++;
                lifestealPet_.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Heal");
            }
        }
        base.WhenEnemyHit(enemy);
    }
}
