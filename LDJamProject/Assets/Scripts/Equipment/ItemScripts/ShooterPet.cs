using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterPet : ItemObjBase
{
    PlayerStats playerStats;
    GameObject shooterPet_;
    [SerializeField] GameObject shooterPet;

    public override void OnPickUp()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        playerStats = tempPlayer.GetComponent<PlayerStats>();
        GameObject spawnPos = GameObject.FindGameObjectWithTag("PetSpawnPos");
        shooterPet_ = Instantiate(shooterPet, spawnPos.transform.position, Quaternion.identity);
        shooterPet_.GetComponent<NavMeshAgent>().stoppingDistance = playerStats.m_NumOfPets * playerStats.m_DistanceBetweenEachPet;
        shooterPet_.GetComponent<FollowBehindPlayer>().destination = spawnPos;
        playerStats.m_NumOfPets++;
        base.OnPickUp();
    }
}
